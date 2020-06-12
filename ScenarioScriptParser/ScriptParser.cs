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

        const string LitteralPatern = "\"(?<litteral>[^\"]*)\"";
        const string ReferencePattern = "(?<ref>\\$[A-z]+)";
        const string ValuePattern = "(?<value>(?:" + LitteralPatern + ")|(?:" + ReferencePattern + "))";
        const string PropertyConditionPattern = "(?<property>[A-z]+)\\s*=\\s*" + ValuePattern;
        const string SingleConditionPattern = "[A-z]+\\s*=\\s*" + ValuePattern;
        const string ConditionPattern = SingleConditionPattern + "(?:\\s*,\\s*" + SingleConditionPattern + ")*";
        const string ParamsDeclarationPattern = "\\(\\s*(?<paramsDecl>" + ReferencePattern + "(?:\\s*,\\s*" + ReferencePattern + ")*)?\\s*\\)"; 
        const string ParamsPattern = "\\(\\s*(?<params>" + ValuePattern + "(?:\\s*,\\s*" + ValuePattern + ")*)?\\s*\\)";
        const string ControlPattern = "(?<controlType>[A-z]+)\\[(?<controlCondition>" + ConditionPattern + ")\\])";
        const string CustomContextPattern = "(?<customContext>(?<contextName>[A-z]+)" + ParamsPattern + "?)";
        const string BaseContextPattern = "(?:" + CustomContextPattern + "|" + ControlPattern + ")";

        const string ContextPattern = BlocIdentifiers.Context + "\\s+(?<name>[A-z]+)\\s*(?:" + ParamsDeclarationPattern + ")?\\s+(?:\\[\\s*(?<rootCondition>" + ConditionPattern + ")\\s*\\])?\\s*?(?:\\s+\\{\\s*(?<uniqueCondition>" + ConditionPattern + ")\\s*\\})?\\s*:";
        const string InteractionPattern = BlocIdentifiers.Interaction + "\\s+(?<name>[A-z]+)\\s*(?:" + ParamsDeclarationPattern + ")?\\s*:";
        const string ScenarioPattern = BlocIdentifiers.Scenario + "\\s+(?<name>[A-z]+)\\s*:";
        const string BaseInteractionPattern = "(?<context>" + BaseContextPattern + "(?:::" + BaseContextPattern + ")*)?::(?<interaction>[A-z]+)" + ParamsPattern;

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

        int FindBlocEnd(List<string> lines, int blocStart)
        {
            int blocStartIndent = CountIndentSpaces(lines[blocStart]);
            var linesSameOrShorterIndent = lines.Where((string line, int index) => index > blocStart && CountIndentSpaces(line) <= blocStartIndent);       
            if (linesSameOrShorterIndent.Count() == 0)
            {
                return lines.Count - 1;
            }
            return lines.IndexOf(linesSameOrShorterIndent.First()) - 1;
        }

        ValueDefinition ParseRuntimeValue(Scope scope, string valueStr)
        {
            Regex valueRegex = new Regex(ValuePattern);
            Match valueMatch = valueRegex.Match(valueStr);
            if (valueMatch.Groups[NamedGroups.Litteral].Success)
            {
                return ValueDefinition.FromLitteral(valueMatch.Groups[NamedGroups.Litteral].Value);
            }
            string referenceName = valueMatch.Groups[NamedGroups.Reference].Value;
            if (!scope.ReferenceValues.ContainsKey(referenceName))
            {
                // TODO: Throw more specific exception.
                throw new Exception($"Use of undeclared reference value \"{referenceName}\".");
            }
            return ValueDefinition.FromReference(referenceName); 
        }

        IEnumerable<ValueDefinition> ParseParamValues(Scope scope, string paramsStr)
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

        IConditionDefinition ParseConditionDefinition(Scope scope, string conditionStr)
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

        IContextDefinition ParseContextDefinitionDeclaration(Scope scope, string line)
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

        IContextDefinition ParseContextDefinition(Scope scope, List<string> lines)
        {
            Scope currentScope = new Scope(scope);
            IContextDefinition contextDefinition = ParseContextDefinitionDeclaration(currentScope, lines[0]);

            for (int curLine = 1; curLine < lines.Count; ++curLine)
            {
                if (string.IsNullOrWhiteSpace(lines[curLine]))
                {
                    continue;
                }

                int blocEnd = FindBlocEnd(lines, curLine);
                List<string> childBloc = lines.GetRange(curLine, blocEnd);

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
                curLine = blocEnd;
            }

            return contextDefinition;
        }

        IInteractionDefinition ParseInteractionDefinition(Scope scope, List<string> lines)
        {
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

            IEnumerable<BaseInteractionDefinition> baseInteractionDefinitions = lines.Skip(1).Select((line) => ParseBaseInteractionDefinition(scope, line));

            return new InteractionDefinition(scope, name, paramNames, baseInteractionDefinitions);
        }

        IBaseContextDefinition ParseBaseContext(Scope scope, IEnumerable<string> contextStrings)
        {
            if (contextStrings.Count() == 0)
            {
                return null;
            }

            string contextStr = contextStrings.First();
            Regex baseContextRegex = new Regex(BaseContextPattern);
            Match baseContextMatch = baseContextRegex.Match(contextStr);

            if (baseContextMatch.Groups[NamedGroups.ControlType].Success && baseContextMatch.Groups[NamedGroups.ControlCondition].Success)
            {
                string controlTypeName = baseContextMatch.Groups[NamedGroups.ControlType].Value;
                string conditionStr = baseContextMatch.Groups[NamedGroups.ControlCondition].Value;
                IConditionDefinition identifyingCondition = ParseConditionDefinition(scope, conditionStr);
                return new BaseControlDefinition(controlTypeName, identifyingCondition, ParseBaseContext(scope, contextStrings.Skip(1)));
            }

            string contextName = baseContextMatch.Groups[NamedGroups.ContextName].Value;
            IEnumerable<ValueDefinition> paramValues = new List<ValueDefinition>();
            if (baseContextMatch.Groups[NamedGroups.Params].Success)
            {
                string paramsStr = baseContextMatch.Groups[NamedGroups.Params].Value;
                paramValues = ParseParamValues(scope, paramsStr);
            }
            return new BaseContextDefinition(contextName, paramValues, ParseBaseContext(scope, contextStrings.Skip(1)));
        }

        BaseInteractionDefinition ParseBaseInteractionDefinition(Scope scope, string line)
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

        IScenarioDefinition ParseScenarioDefinition(Scope scope, List<string> lines)
        {
            string declarationLine = lines[0].Trim();
            Regex scenarioRegex = new Regex(ScenarioPattern);
            Match scenarioMatch = scenarioRegex.Match(declarationLine);

            string name = scenarioMatch.Groups[NamedGroups.Name].Value;
            IEnumerable<BaseInteractionDefinition> baseInteractionDefinitions = lines.Select((line) => ParseBaseInteractionDefinition(scope, line));
            
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

                int blocEnd = FindBlocEnd(lines, curLine);
                if (IsScenario(lines[curLine]))
                {
                    IScenarioDefinition scenarioDefinition = ParseScenarioDefinition(script.RootScope, lines.GetRange(curLine, blocEnd));
                    script.ScenarioDefinitions.Add(scenarioDefinition.Name, scenarioDefinition);
                }
                else if (IsContext(lines[curLine]))
                {
                    IContextDefinition contextDefinition = ParseContextDefinition(script.RootScope, lines.GetRange(curLine, blocEnd));
                    script.RootScope.ContextDefinitions.Add(contextDefinition.Name, contextDefinition);
                }

                curLine = blocEnd;
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
