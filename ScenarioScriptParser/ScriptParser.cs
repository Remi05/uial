using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using ScenarioScripting;
using ScenarioScripting.Conditions;
using ScenarioScripting.Contexts;
using ScenarioScripting.Interactions;
using ScenarioScripting.Scenarios;
using ScenarioScripting.Scopes;

namespace ScenarioScriptParser
{
    public class ScriptParser
    {
        static class BlocIdentifiers
        {
            public const string Context = "context";
            public const string Interaction = "interaction";
            public const string Scenario = "scenario";
        }

        static class NamedGroups
        {
            public const string Context = "context";
            public const string ContextName = "contextName";
            public const string CustomContext = "customContext";
            public const string ControlCondition = "controlCondition";
            public const string ControlType = "controlType";
            public const string Interaction = "interaction";
            public const string Litteral = "litteral";
            public const string Name = "name";
            public const string Params = "params";
            public const string ParamsDeclaration = "paramsDecl";
            public const string Property = "property";
            public const string Reference = "ref";
            public const string RootElementCondition = "rootCondition";
            public const string UniqueCondition = "uniqueCondition";
            public const string Value = "value";
        }

        const string LitteralPattern = "\"(?<litteral>[^\"]*)\"";
        const string ReferencePattern = "(?<ref>\\$[a-zA-Z]+)";
        const string ValuePattern = "(?<value>(?:" + LitteralPattern + ")|(?:" + ReferencePattern + "))";
        const string PropertyConditionPattern = "(?<property>[a-zA-Z]+)\\s*=\\s*" + ValuePattern;
        const string SingleConditionPattern = "[a-zA-Z]+\\s*=\\s*" + ValuePattern;
        const string ConditionPattern = SingleConditionPattern + "(?:\\s*,\\s*" + SingleConditionPattern + ")*";
        const string ParamsDeclarationPattern = "\\(\\s*(?<paramsDecl>" + ReferencePattern + "(?:\\s*,\\s*" + ReferencePattern + ")*)?\\s*\\)"; 
        const string ParamsPattern = "(?:\\(\\s*(?<params>" + ValuePattern + "(?:\\s*,\\s*" + ValuePattern + ")*)?\\s*\\))";
        const string ControlPattern = "((?<controlType>[a-zA-Z]+)\\[(?<controlCondition>" + ConditionPattern + ")\\])";
        const string CustomContextPattern = "(?<customContext>(?<contextName>[a-zA-Z]+)" + ParamsPattern + "?)";
        const string BaseContextPattern = "(?:" + CustomContextPattern + "|" + ControlPattern + ")";

        const string ContextPattern = BlocIdentifiers.Context + "\\s+(?<name>[a-zA-Z]+)\\s*(?:" + ParamsDeclarationPattern + ")?\\s+(?:\\[\\s*(?<rootCondition>" + ConditionPattern + ")\\s*\\])?\\s*?(?:\\s+\\{\\s*(?<uniqueCondition>" + ConditionPattern + ")\\s*\\})?\\s*:";
        const string InteractionPattern = BlocIdentifiers.Interaction + "\\s+(?<name>[a-zA-Z]+)\\s*(?:" + ParamsDeclarationPattern + ")?\\s*:";
        const string ScenarioPattern = BlocIdentifiers.Scenario + "\\s+(?<name>[a-zA-Z]+)\\s*:";
        const string BaseInteractionPattern = "(?<context>" + BaseContextPattern + "(?:::" + BaseContextPattern + ")*)?::(?<interaction>[a-zA-Z]+)" + ParamsPattern;

        bool IsContext(string line)
        {
            return new Regex(ContextPattern).Match(line).Success;
        }

        bool IsInteraction(string line)
        {
            return new Regex(InteractionPattern).Match(line).Success;
        }

        bool IsScenario(string line)
        {
            return new Regex(ScenarioPattern).Match(line).Success;
        }

        int CountIndentSpaces(string line)
        {
            return line.TakeWhile(char.IsWhiteSpace).Count();
        }

        int FindBlocLength(List<string> lines, int blocStart)
        {
            int blocStartIndent = CountIndentSpaces(lines[blocStart]);
            int blocEnd = blocStart;
            for (int i = blocStart + 1; i < lines.Count; ++i)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]))
                {
                    if (CountIndentSpaces(lines[i]) <= blocStartIndent)
                    {
                        break;
                    }
                    blocEnd = i;
                }
            }
            return blocEnd - blocStart + 1;
        }

        ValueDefinition ParseRuntimeValue(DefinitionScope scope, string valueStr)
        {
            Regex valueRegex = new Regex(ValuePattern);
            Match valueMatch = valueRegex.Match(valueStr);
            if (valueMatch.Groups[NamedGroups.Litteral].Success)
            {
                return ValueDefinition.FromLitteral(valueMatch.Groups[NamedGroups.Litteral].Value);
            }
            string referenceName = valueMatch.Groups[NamedGroups.Reference].Value;
            return ValueDefinition.FromReference(referenceName); 
        }

        IEnumerable<ValueDefinition> ParseParamValues(DefinitionScope scope, string paramsStr)
        {
            List<ValueDefinition> paramValues = new List<ValueDefinition>();
            Regex valueRegex = new Regex(ValuePattern);
            MatchCollection matches = valueRegex.Matches(paramsStr);
            foreach (Match match in matches)
            {
                paramValues.Add(ParseRuntimeValue(scope, match.Value));
            }
            return paramValues;
        }

        IEnumerable<string> ParseParamsDeclaration(string paramsStr)
        {
            List<string> paramNames = new List<string>();
            Regex paramRegex = new Regex(ReferencePattern);
            MatchCollection matches = paramRegex.Matches(paramsStr);
            foreach (Match match in matches)
            {
                paramNames.Add(match.Value);
            }
            return paramNames;
        }

        IConditionDefinition ParseConditionDefinition(DefinitionScope scope, string conditionStr)
        {
            Regex conditionRegex = new Regex(PropertyConditionPattern);
            MatchCollection matches = conditionRegex.Matches(conditionStr);
            List<IConditionDefinition> conditionDefinitions = new List<IConditionDefinition>(matches.Count);
            foreach (Match match in matches)
            {
                AutomationProperty property = Controls.GetPropertyByName(match.Groups[NamedGroups.Property].Value);
                ValueDefinition value = ParseRuntimeValue(scope, match.Groups[NamedGroups.Value].Value);
                conditionDefinitions.Add(new PropertyConditionDefinition(property, value));
            }
            return new CompositeConditionDefinition(conditionDefinitions);
        }

        IInteractionDefinition ParseInteractionDefinition(DefinitionScope scope, List<string> lines)
        {
            DefinitionScope currentScope = new DefinitionScope(scope);

            string declarationLine = lines[0].Trim();
            Regex interactionRegex = new Regex(InteractionPattern);
            Match interactionMatch = interactionRegex.Match(declarationLine);
            
            string name = interactionMatch.Groups[NamedGroups.Name].Value;

            IEnumerable<string> paramNames = new List<string>();
            if (interactionMatch.Groups[NamedGroups.ParamsDeclaration].Success)
            {
                string paramsStr = interactionMatch.Groups[NamedGroups.ParamsDeclaration].Value;
                paramNames = ParseParamsDeclaration(paramsStr);
            }

            IEnumerable<BaseInteractionDefinition> baseInteractionDefinitions = lines.Skip(1).Select((line) => ParseBaseInteractionDefinition(currentScope, line));

            return new InteractionDefinition(currentScope, name, paramNames, baseInteractionDefinitions);
        }

        IContextDefinition ParseContextDefinitionDeclaration(DefinitionScope scope, string line)
        {
            line = line.Trim();
            Regex contextRegex = new Regex(ContextPattern);
            Match contextMatch = contextRegex.Match(line);

            string name = contextMatch.Groups[NamedGroups.Name].Value;

            IEnumerable<string> paramNames = new List<string>();
            if (contextMatch.Groups[NamedGroups.ParamsDeclaration].Success)
            {
                string paramsStr = contextMatch.Groups[NamedGroups.ParamsDeclaration].Value;
                paramNames = ParseParamsDeclaration(paramsStr);
            }

            IConditionDefinition rootElementCondition = null;
            if (contextMatch.Groups[NamedGroups.RootElementCondition].Success)
            {
                string rootElementConditionStr = contextMatch.Groups[NamedGroups.RootElementCondition].Value;
                rootElementCondition = ParseConditionDefinition(scope, rootElementConditionStr);
            }

            IConditionDefinition uniqueCondition = null;
            if (contextMatch.Groups[NamedGroups.UniqueCondition].Success)
            {
                string uniqueConditionStr = contextMatch.Groups[NamedGroups.UniqueCondition].Value;
                uniqueCondition = ParseConditionDefinition(scope, uniqueConditionStr);
            }

            return new ContextDefinition(scope, name, paramNames, rootElementCondition, uniqueCondition);
        }

        IContextDefinition ParseContextDefinition(DefinitionScope scope, List<string> lines)
        {
            DefinitionScope currentScope = new DefinitionScope(scope);
            IContextDefinition contextDefinition = ParseContextDefinitionDeclaration(currentScope, lines[0]);

            for (int curLine = 1; curLine < lines.Count; ++curLine)
            {
                if (string.IsNullOrWhiteSpace(lines[curLine]))
                {
                    continue;
                }

                int blocLength = FindBlocLength(lines, curLine);
                List<string> childBloc = lines.GetRange(curLine, blocLength);

                if (IsContext(lines[curLine]))
                {
                    IContextDefinition childContextDefinition = ParseContextDefinition(currentScope, childBloc);
                    currentScope.ContextDefinitions.Add(childContextDefinition.Name, childContextDefinition);
                }
                else if (IsInteraction(lines[curLine]))
                {
                    IInteractionDefinition interactionDefinition = ParseInteractionDefinition(currentScope, childBloc);
                    currentScope.InteractionDefinitions.Add(interactionDefinition.Name, interactionDefinition);
                }
                else
                {
                    throw new Exception(); // TODO: Specify exception
                }
                curLine += blocLength - 1;
            }

            return contextDefinition;
        }

        IBaseContextDefinition ParseBaseContext(DefinitionScope scope, IEnumerable<string> contextStrings)
        {
            if (contextStrings.Count() == 0)
            {
                return null;
            }

            string contextStr = contextStrings.First();

            Regex controlRegex = new Regex(ControlPattern);
            Match controlMatch = controlRegex.Match(contextStr);
            if (controlMatch.Success)
            {
                string controlTypeName = controlMatch.Groups[NamedGroups.ControlType].Value;
                string conditionStr = controlMatch.Groups[NamedGroups.ControlCondition].Value;
                IConditionDefinition identifyingCondition = ParseConditionDefinition(scope, conditionStr);
                return new BaseControlDefinition(controlTypeName, identifyingCondition, ParseBaseContext(scope, contextStrings.Skip(1)));
            }

            Regex customContextRegex = new Regex(CustomContextPattern);
            Match customContextMatch = customContextRegex.Match(contextStr);
            string contextName = customContextMatch.Groups[NamedGroups.ContextName].Value;

            IEnumerable<ValueDefinition> paramValues = new List<ValueDefinition>();
            if (customContextMatch.Groups[NamedGroups.Params].Success)
            {
                string paramsStr = customContextMatch.Groups[NamedGroups.Params].Value;
                paramValues = ParseParamValues(scope, paramsStr);
            }
            return new BaseContextDefinition(contextName, paramValues, ParseBaseContext(scope, contextStrings.Skip(1)));
        }

        BaseInteractionDefinition ParseBaseInteractionDefinition(DefinitionScope scope, string line)
        {
            Regex baseInteractionRegex = new Regex(BaseInteractionPattern);
            Match baseInteractionMatch = baseInteractionRegex.Match(line);
            
            string interactionName = baseInteractionMatch.Groups[NamedGroups.Interaction].Value;

            IBaseContextDefinition interactionContext = null;
            if (baseInteractionMatch.Groups[NamedGroups.Context].Success)
            {
                string composedContextStr = baseInteractionMatch.Groups[NamedGroups.Context].Value;
                string[] contextStrings = composedContextStr.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
                interactionContext = ParseBaseContext(scope, contextStrings);
            }

            IEnumerable<ValueDefinition> paramValues = new List<ValueDefinition>();
            if (baseInteractionMatch.Groups[NamedGroups.Params].Success)
            {
                string paramsStr = baseInteractionMatch.Groups[NamedGroups.Params].Value;
                paramValues = ParseParamValues(scope, paramsStr);
            }

            return new BaseInteractionDefinition(interactionName, paramValues, interactionContext);
        }

        IScenarioDefinition ParseScenarioDefinition(DefinitionScope scope, List<string> lines)
        {
            string declarationLine = lines[0].Trim();
            Regex scenarioRegex = new Regex(ScenarioPattern);
            Match scenarioMatch = scenarioRegex.Match(declarationLine);

            string name = scenarioMatch.Groups[NamedGroups.Name].Value;
            IEnumerable<BaseInteractionDefinition> baseInteractionDefinitions = lines.Skip(1).Select((line) => ParseBaseInteractionDefinition(scope, line));
            
            return new ScenarioDefinition(name, baseInteractionDefinitions);
        }

        Script ParseScript(List<string> lines)
        {
            Script script = new Script();

            for (int curLine = 0; curLine < lines.Count; ++curLine)
            {
                if (string.IsNullOrWhiteSpace(lines[curLine]))
                {
                    continue;
                }

                int blocLength = FindBlocLength(lines, curLine);
                if (IsScenario(lines[curLine]))
                {
                    IScenarioDefinition scenarioDefinition = ParseScenarioDefinition(script.RootScope, lines.GetRange(curLine, blocLength));
                    script.ScenarioDefinitions.Add(scenarioDefinition.Name, scenarioDefinition);
                }
                else if (IsContext(lines[curLine]))
                {
                    IContextDefinition contextDefinition = ParseContextDefinition(script.RootScope, lines.GetRange(curLine, blocLength));
                    script.RootScope.ContextDefinitions.Add(contextDefinition.Name, contextDefinition);
                }

                curLine += blocLength - 1;
            }

            return script;
        }

        public Script ParseScript(StreamReader streamReader)
        {
            List<string> lines = streamReader.ReadToEnd().Split('\n').ToList();
            lines = lines.Select((string line) => line.Replace("\t", new string(' ', 4))).ToList();
            return ParseScript(lines);
        }
    }
}
