using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using Uial.Conditions;
using Uial.Contexts;
using Uial.Interactions;
using Uial.Parsing.Exceptions;
using Uial.Scenarios;
using Uial.Scopes;

namespace Uial.Parsing
{
    public class ScriptParser : IScriptParser
    {
        static class BlocIdentifiers
        {
            public const string Comment = "//";
            public const string Context = "context";
            public const string Import = "import";
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
            public const string ImportName = "importName"; 
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

        const string IsolatedLitteralPattern = "^\"(?<litteral>[^\"]*)\"$";
        const string IsolatedReferencePattern = "^(?<ref>\\$[a-zA-Z]+(?:[0-9]+)?)$";
        const string IsolatedValuePattern = "(?<value>(?:" + IsolatedLitteralPattern + ")|(?:" + IsolatedReferencePattern + "))";
        const string LitteralPattern = "\"(?<litteral>[^\"]*)\"";
        const string ReferencePattern = "(?<ref>\\$[a-zA-Z]+(?:[0-9]+)?)";
        const string ValuePattern = "(?<value>(?:" + LitteralPattern + ")|(?:" + ReferencePattern + "))";
        const string PropertyConditionPattern = "(?<property>[a-zA-Z]+)\\s*=\\s*" + ValuePattern;
        const string SingleConditionPattern = "[a-zA-Z]+\\s*=\\s*" + ValuePattern;
        const string ConditionPattern = SingleConditionPattern + "(?:\\s*,\\s*" + SingleConditionPattern + ")*";
        const string ParamsDeclarationPattern = "\\(\\s*(?<paramsDecl>" + ReferencePattern + "(?:\\s*,\\s*" + ReferencePattern + ")*)?\\s*\\)"; 
        const string ParamsPattern = "(?:\\(\\s*(?<params>" + ValuePattern + "(?:\\s*,\\s*" + ValuePattern + ")*)?\\s*\\))";
        const string ControlPattern = "((?<controlType>[a-zA-Z]+)\\[(?<controlCondition>" + ConditionPattern + ")\\])";
        const string CustomContextPattern = "(?<customContext>(?<contextName>[a-zA-Z]+)" + ParamsPattern + "?)";
        const string BaseContextPattern = "(?:" + CustomContextPattern + "|" + ControlPattern + ")";
        const string ImportNamePattern = "(?<importName>(?:[a-zA-Z0-9\\.]+/)*[a-zA-Z0-9]+\\.uial)";
        const string BlocNamePattern = "(?<name>[a-zA-Z]+)";

        const string ImportPattern = BlocIdentifiers.Import + "\\s+'" + ImportNamePattern + "'";
        const string ContextPattern = BlocIdentifiers.Context + "\\s+" + BlocNamePattern + "\\s*(?:" + ParamsDeclarationPattern + ")?(?:\\s+\\[\\s*(?<rootCondition>" + ConditionPattern + ")\\s*\\])?(?:\\s+\\{\\s*(?<uniqueCondition>" + ConditionPattern + ")\\s*\\})?\\s*:\\s*$";
        const string InteractionPattern = BlocIdentifiers.Interaction + "\\s+" + BlocNamePattern + "\\s*(?:" + ParamsDeclarationPattern + ")?\\s*:";
        const string ScenarioPattern = BlocIdentifiers.Scenario + "\\s+" + BlocNamePattern + "\\s*:";
        const string BaseInteractionPattern = "^\\s*(?<context>" + BaseContextPattern + "(?:::" + BaseContextPattern + ")*)?::(?<interaction>[a-zA-Z]+)" + ParamsPattern + "\\s*$";

        private bool IsComment(string line)
        {
            return line.Trim().StartsWith(BlocIdentifiers.Comment);
        }

        private bool IsImport(string line)
        {
            return line.Trim().StartsWith(BlocIdentifiers.Import);
        }

        private bool IsContext(string line)
        {
            return line.Trim().StartsWith(BlocIdentifiers.Context);
        }

        private bool IsInteraction(string line)
        {
            return line.Trim().StartsWith(BlocIdentifiers.Interaction);
        }

        private bool IsScenario(string line)
        {
            return line.Trim().StartsWith(BlocIdentifiers.Scenario);
        }

        private int CountIndentSpaces(string line)
        {
            return line.TakeWhile(char.IsWhiteSpace).Count();
        }

        private int FindBlocLength(List<string> lines, int blocStart)
        {
            int blocStartIndent = CountIndentSpaces(lines[blocStart]);
            int blocEnd = blocStart;
            for (int i = blocStart + 1; i < lines.Count; ++i)
            {
                if (!string.IsNullOrWhiteSpace(lines[i]) && !IsComment(lines[i]))
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

        public ValueDefinition ParseValueDefinition(string valueStr)
        {
            Regex valueRegex = new Regex(IsolatedValuePattern);
            Match valueMatch = valueRegex.Match(valueStr);
            if (!valueMatch.Success)
            {
                throw new InvalidValueDefinitionException(valueStr);
            }

            if (valueMatch.Groups[NamedGroups.Litteral].Success)
            {
                return ValueDefinition.FromLitteral(valueMatch.Groups[NamedGroups.Litteral].Value);
            }
            string referenceName = valueMatch.Groups[NamedGroups.Reference].Value;
            return ValueDefinition.FromReference(referenceName); 
        }

        public IEnumerable<ValueDefinition> ParseParamValues(string paramsStr)
        {
            List<ValueDefinition> paramValues = new List<ValueDefinition>();
            Regex valueRegex = new Regex(ValuePattern);
            MatchCollection matches = valueRegex.Matches(paramsStr);
            foreach (Match match in matches)
            {
                paramValues.Add(ParseValueDefinition(match.Value));
            }
            return paramValues;
        }

        public IEnumerable<string> ParseParamsDeclaration(string paramsStr)
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

        public IConditionDefinition ParseConditionDefinition(string conditionStr)
        {
            Regex conditionRegex = new Regex(PropertyConditionPattern);
            MatchCollection matches = conditionRegex.Matches(conditionStr);
            if (matches.Count == 0)
            {
                throw new InvalidConditionException(conditionStr);
            }

            List<IConditionDefinition> conditionDefinitions = new List<IConditionDefinition>(matches.Count);
            foreach (Match match in matches)
            {
                AutomationProperty property = Properties.GetPropertyByName(match.Groups[NamedGroups.Property].Value);
                ValueDefinition value = ParseValueDefinition(match.Groups[NamedGroups.Value].Value);
                conditionDefinitions.Add(new PropertyConditionDefinition(property, value));
            }

            return new CompositeConditionDefinition(conditionDefinitions);
        }

        public IInteractionDefinition ParseInteractionDefinition(DefinitionScope scope, List<string> lines)
        {
            if (lines.Count == 0)
            {
                throw new Exception("Could not parse interaction definition, no lines provided.");
            }

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

            IEnumerable<IBaseInteractionDefinition> baseInteractionDefinitions = lines.Skip(1)
                .Where((line) => !string.IsNullOrWhiteSpace(line) && !IsComment(line))
                .Select((line) => ParseBaseInteractionDefinition(line));

            return new InteractionDefinition(currentScope, name, paramNames, baseInteractionDefinitions);
        }

        public IContextDefinition ParseContextDefinitionDeclaration(DefinitionScope scope, string line)
        {
            line = line.Trim();
            Regex contextRegex = new Regex(ContextPattern);
            Match contextMatch = contextRegex.Match(line);
            if (!contextMatch.Success)
            {
                throw new InvalidContextDeclarationException(line);
            }

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
                rootElementCondition = ParseConditionDefinition(rootElementConditionStr);
            }

            IConditionDefinition uniqueCondition = null;
            if (contextMatch.Groups[NamedGroups.UniqueCondition].Success)
            {
                string uniqueConditionStr = contextMatch.Groups[NamedGroups.UniqueCondition].Value;
                uniqueCondition = ParseConditionDefinition(uniqueConditionStr);
            }

            return new ContextDefinition(scope, name, paramNames, rootElementCondition, uniqueCondition);
        }

        public IContextDefinition ParseContextDefinition(DefinitionScope scope, List<string> lines)
        {
            DefinitionScope currentScope = new DefinitionScope(scope);
            IContextDefinition contextDefinition = ParseContextDefinitionDeclaration(currentScope, lines[0]);

            for (int curLine = 1; curLine < lines.Count; ++curLine)
            {
                if (string.IsNullOrWhiteSpace(lines[curLine]) || IsComment(lines[curLine]))
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
                    throw new InvalidContextDefinitionException(string.Join("\n", lines));
                }
                curLine += blocLength - 1;
            }

            return contextDefinition;
        }

        private IBaseContextDefinition ParseBaseContextDefinition(IEnumerable<string> contextStrings)
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
                IConditionDefinition identifyingCondition = ParseConditionDefinition(conditionStr);
                return new BaseControlDefinition(controlTypeName, identifyingCondition, ParseBaseContextDefinition(contextStrings.Skip(1)));
            }

            Regex customContextRegex = new Regex(CustomContextPattern);
            Match customContextMatch = customContextRegex.Match(contextStr);
            string contextName = customContextMatch.Groups[NamedGroups.ContextName].Value;

            IEnumerable<ValueDefinition> paramValues = new List<ValueDefinition>();
            if (customContextMatch.Groups[NamedGroups.Params].Success)
            {
                string paramsStr = customContextMatch.Groups[NamedGroups.Params].Value;
                paramValues = ParseParamValues(paramsStr);
            }
            return new BaseContextDefinition(contextName, paramValues, ParseBaseContextDefinition(contextStrings.Skip(1)));
        }

        public IBaseContextDefinition ParseBaseContextDefinition(string baseContextStr)
        {
            string[] contextStrings = baseContextStr.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);
            return ParseBaseContextDefinition(contextStrings);
        }

        public IBaseInteractionDefinition ParseBaseInteractionDefinition(string line)
        {
            Regex baseInteractionRegex = new Regex(BaseInteractionPattern);
            Match baseInteractionMatch = baseInteractionRegex.Match(line);
            if (!baseInteractionMatch.Success)
            {
                throw new InvalidBaseInteractionException(line);
            }
            
            string interactionName = baseInteractionMatch.Groups[NamedGroups.Interaction].Value;

            IBaseContextDefinition baseContextDefinition = null;
            if (baseInteractionMatch.Groups[NamedGroups.Context].Success)
            {
                string baseContextStr = baseInteractionMatch.Groups[NamedGroups.Context].Value;
                baseContextDefinition = ParseBaseContextDefinition(baseContextStr);
            }

            IEnumerable<ValueDefinition> paramValues = new List<ValueDefinition>();
            if (baseInteractionMatch.Groups[NamedGroups.Params].Success)
            {
                string paramsStr = baseInteractionMatch.Groups[NamedGroups.Params].Value;
                paramValues = ParseParamValues(paramsStr);
            }

            return new BaseInteractionDefinition(interactionName, paramValues, baseContextDefinition);
        }

        public IScenarioDefinition ParseScenarioDefinition(List<string> lines)
        {
            string declarationLine = lines[0].Trim();
            Regex scenarioRegex = new Regex(ScenarioPattern);
            Match scenarioMatch = scenarioRegex.Match(declarationLine);

            string name = scenarioMatch.Groups[NamedGroups.Name].Value;
            IEnumerable<IBaseInteractionDefinition> baseInteractionDefinitions = lines.Skip(1)
                .Where((line) => !string.IsNullOrWhiteSpace(line) && !IsComment(line))
                .Select((line) => ParseBaseInteractionDefinition(line));
            
            return new ScenarioDefinition(name, baseInteractionDefinitions);
        }

        public Script ParseImport(string importStr, string executionDirPath)
        {
            Regex importRegex = new Regex(ImportPattern);
            Match importMatch = importRegex.Match(importStr);
            string importName = importMatch.Groups[NamedGroups.ImportName].Value;
            string importFilePath = Path.Combine(executionDirPath, importName);
            return ParseScript(importFilePath);
        }

        public Script ParseScript(List<string> lines, string executionDirPath)
        {
            Script script = new Script();

            for (int curLine = 0; curLine < lines.Count; ++curLine)
            {
                if (string.IsNullOrWhiteSpace(lines[curLine]) || IsComment(lines[curLine]))
                {
                    continue;
                }

                if (IsImport(lines[curLine]))
                {
                    Script importedScript = ParseImport(lines[curLine], executionDirPath);
                    script.AddScript(importedScript);
                    continue;
                }

                int blocLength = FindBlocLength(lines, curLine);
                if (IsScenario(lines[curLine]))
                {
                    IScenarioDefinition scenarioDefinition = ParseScenarioDefinition(lines.GetRange(curLine, blocLength));
                    script.ScenarioDefinitions.Add(scenarioDefinition.Name, scenarioDefinition);
                }
                else if (IsContext(lines[curLine]))
                {
                    IContextDefinition contextDefinition = ParseContextDefinition(script.RootScope, lines.GetRange(curLine, blocLength));
                    script.RootScope.ContextDefinitions.Add(contextDefinition.Name, contextDefinition);
                }
                else
                {
                    throw new UnrecognizedPatternExeception(lines[curLine]);
                }

                curLine += blocLength - 1;
            }

            return script;
        }

        public Script ParseScript(StreamReader streamReader, string executionDirPath)
        {
            List<string> lines = streamReader.ReadToEnd().Split('\n').ToList();
            lines = lines.Select((string line) => line.Replace("\t", new string(' ', 4))).ToList();
            return ParseScript(lines, executionDirPath);
        }

        public Script ParseScript(string filePath)
        {
            string executionDirPath = Path.GetDirectoryName(filePath);

            Script script;
            using (StreamReader fileReader = new StreamReader(filePath))
            {
                script = ParseScript(fileReader, executionDirPath);
            }
            return script;
        }
    }
}
