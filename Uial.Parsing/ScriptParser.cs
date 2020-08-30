using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using Uial.Conditions;
using Uial.Contexts;
using Uial.Contexts.Windows;
using Uial.Interactions;
using Uial.Parsing.Exceptions;
using Uial.Scenarios;
using Uial.Scopes;
using Uial.Testing;

namespace Uial.Parsing
{
    public class ScriptParser : IScriptParser
    {
        static class BlocIdentifiers
        {
            public const string Assertion = "Assert";
            public const string Comment = "//";
            public const string Context = "context";
            public const string Import = "import";
            public const string Interaction = "interaction";
            public const string Scenario = "scenario";
            public const string Test = "test";
            public const string TestGroup = "testgroup";
        }

        static class NamedGroups
        {
            public const string Context = "context";
            public const string ContextName = "contextName";
            public const string ContextParams = "contextParams";
            public const string CustomContext = "customContext";
            public const string ControlCondition = "controlCondition";
            public const string ControlType = "controlType";
            public const string Interaction = "interaction";
            public const string Literal = "literal";
            public const string Name = "name";
            public const string Params = "params";
            public const string ParamsDeclaration = "paramsDecl";
            public const string Path = "path";
            public const string Property = "property";
            public const string Reference = "ref";
            public const string RepoName = "repoName";
            public const string RootElementCondition = "rootCondition";
            public const string UniqueCondition = "uniqueCondition";
            public const string Value = "value";
        }

        const string IsolatedLiteralPattern = "^\"(?<literal>[^\"]*)\"$";
        const string IsolatedReferencePattern = "^(?<ref>\\$[a-zA-Z]+(?:[0-9]+)?)$";
        const string IsolatedValuePattern = "(?<value>(?:" + IsolatedLiteralPattern + ")|(?:" + IsolatedReferencePattern + "))";
        const string LiteralPattern = "\"(?<literal>[^\"]*)\"";
        const string ReferencePattern = "(?<ref>\\$[a-zA-Z]+(?:[0-9]+)?)";
        const string ValuePattern = "(?<value>(?:" + LiteralPattern + ")|(?:" + ReferencePattern + "))";
        const string PropertyConditionPattern = "(?<property>[a-zA-Z]+)\\s*=\\s*" + ValuePattern;
        const string SingleConditionPattern = "[a-zA-Z]+\\s*=\\s*" + ValuePattern;
        const string ConditionPattern = SingleConditionPattern + "(?:\\s*,\\s*" + SingleConditionPattern + ")*";
        const string ParamsDeclarationPattern = "\\(\\s*(?<paramsDecl>" + ReferencePattern + "(?:\\s*,\\s*" + ReferencePattern + ")*)?\\s*\\)"; 
        const string ContextParamsPattern = "(?:\\(\\s*(?<contextParams>" + ValuePattern + "(?:\\s*,\\s*" + ValuePattern + ")*)?\\s*\\))";
        const string ParamsPattern = "(?:\\(\\s*(?<params>" + ValuePattern + "(?:\\s*,\\s*" + ValuePattern + ")*)?\\s*\\))";
        const string ControlPattern = "((?<controlType>[a-zA-Z]+)\\[(?<controlCondition>" + ConditionPattern + ")\\])";
        const string CustomContextPattern = "(?<customContext>(?<contextName>[a-zA-Z]+)" + ContextParamsPattern + "?)";
        const string BaseContextPattern = "(?:" + CustomContextPattern + "|" + ControlPattern + ")";
        const string PathPattern = "(?<path>(?:[a-zA-Z0-9\\._-]+/)*[a-zA-Z0-9\\._-]+\\.uial)";
        const string RepoPathPattern = "github:(?<repoName>[a-zA-Z0-9-_]+/[a-zA-Z0-9-_]+)/" + PathPattern;
        const string BlocNamePattern = "(?<name>[a-zA-Z]+)";

        const string RepoImportPattern = BlocIdentifiers.Import + "\\s+'" + RepoPathPattern + "'"; 
        const string LocalImportPattern = BlocIdentifiers.Import + "\\s+'" + PathPattern + "'";
        const string ContextPattern = BlocIdentifiers.Context + "\\s+" + BlocNamePattern + "\\s*(?:" + ParamsDeclarationPattern + ")?(?:\\s+\\[\\s*(?<rootCondition>" + ConditionPattern + ")\\s*\\])?(?:\\s+\\{\\s*(?<uniqueCondition>" + ConditionPattern + ")\\s*\\})?\\s*:\\s*$";
        const string InteractionPattern = BlocIdentifiers.Interaction + "\\s+" + BlocNamePattern + "\\s*(?:" + ParamsDeclarationPattern + ")?\\s*:";
        const string ScenarioPattern = BlocIdentifiers.Scenario + "\\s+" + BlocNamePattern + "\\s*:";
        const string TestPattern = BlocIdentifiers.Test + "\\s+" + BlocNamePattern + "\\s*:";
        const string TestGroupPattern = BlocIdentifiers.TestGroup + "\\s+" + BlocNamePattern + "\\s*:";
        const string BaseInteractionPattern = "^\\s*(?<context>" + BaseContextPattern + "(?:::" + BaseContextPattern + ")*)?::(?<interaction>[a-zA-Z]+)" + ParamsPattern + "\\s*$";


        public bool IsAssertion(string line)
        {
            return line.Trim().StartsWith(BlocIdentifiers.Assertion);
        }

        public bool IsBaseContext(string line)
        {
            var baseContextRegex = new Regex(BaseContextPattern);
            return baseContextRegex.IsMatch(line);
        }

        public bool IsBaseInteraction(string line)
        {
            var baseInteractionRegex = new Regex(BaseInteractionPattern);
            return baseInteractionRegex.IsMatch(line);
        }

        public bool IsComment(string line)
        {
            return line.Trim().StartsWith(BlocIdentifiers.Comment);
        }

        public bool IsImport(string line)
        {
            return line.Trim().StartsWith(BlocIdentifiers.Import);
        }

        public bool IsCondition(string line)
        {
            var conditionRegex = new Regex(ConditionPattern);
            return conditionRegex.IsMatch(line);
        }

        public bool IsContext(string line)
        {
            return line.Trim().StartsWith(BlocIdentifiers.Context);
        }

        public bool IsInteraction(string line)
        {
            return line.Trim().StartsWith(BlocIdentifiers.Interaction);
        }

        public bool IsScenario(string line)
        {
            return line.Trim().StartsWith(BlocIdentifiers.Scenario);
        }

        public bool IsTest(string line)
        {
            return line.Trim().StartsWith(BlocIdentifiers.Test);
        }

        public bool IsTestGroup(string line)
        {
            return line.Trim().StartsWith(BlocIdentifiers.TestGroup);
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

            if (valueMatch.Groups[NamedGroups.Literal].Success)
            {
                return ValueDefinition.FromLiteral(valueMatch.Groups[NamedGroups.Literal].Value);
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
            if (customContextMatch.Groups[NamedGroups.ContextParams].Success)
            {
                string paramsStr = customContextMatch.Groups[NamedGroups.ContextParams].Value;
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

            IEnumerable<ValueDefinition> paramValues = new List<ValueDefinition>();
            if (baseInteractionMatch.Groups[NamedGroups.Params].Success)
            {
                string paramsStr = baseInteractionMatch.Groups[NamedGroups.Params].Value;
                paramValues = ParseParamValues(paramsStr);
            }

            if (IsAssertion(line))
            {
                return new AssertionDefinition(interactionName, paramValues);
            }

            IBaseContextDefinition baseContextDefinition = null;
            if (baseInteractionMatch.Groups[NamedGroups.Context].Success)
            {
                string baseContextStr = baseInteractionMatch.Groups[NamedGroups.Context].Value;
                baseContextDefinition = ParseBaseContextDefinition(baseContextStr);
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

        public TestDefinition ParseTestDefinition(List<string> lines)
        {
            string declarationLine = lines[0].Trim();
            Regex testRegex = new Regex(TestPattern);
            Match testMatch = testRegex.Match(declarationLine);
            if (!testMatch.Success)
            {
                throw new InvalidTestDefinitionException(declarationLine);
            }

            string name = testMatch.Groups[NamedGroups.Name].Value;
            IEnumerable<IBaseInteractionDefinition> baseInteractionDefinitions = lines.Skip(1)
                .Where((line) => !string.IsNullOrWhiteSpace(line) && !IsComment(line))
                .Select((line) => ParseBaseInteractionDefinition(line));

            return new TestDefinition(name, baseInteractionDefinitions);
        }

        public TestGroupDefinition ParseTestGroupDefinition(List<string> lines)
        {
            string declarationLine = lines[0].Trim();
            Regex testGroupRegex = new Regex(TestGroupPattern);
            Match testGroupMatch = testGroupRegex.Match(declarationLine);
            if (!testGroupMatch.Success)
            {
                throw new InvalidTestGroupDeclarationException(declarationLine);
            }

            string name = testGroupMatch.Groups[NamedGroups.Name].Value;

            List<ITestableDefinition> childrenfinitions = new List<ITestableDefinition>();
            for (int curLine = 1; curLine < lines.Count; ++curLine)
            {
                if (string.IsNullOrWhiteSpace(lines[curLine]) || IsComment(lines[curLine]))
                {
                    continue;
                }

                int blocLength = FindBlocLength(lines, curLine);
                List<string> childBloc = lines.GetRange(curLine, blocLength);

                ITestableDefinition childDefinition;
                if (IsTestGroup(lines[curLine]))
                {
                    childDefinition = ParseTestGroupDefinition(childBloc);
                }
                else if (IsTest(lines[curLine]))
                {
                    childDefinition = ParseTestDefinition(childBloc);
                }
                else
                {
                    throw new InvalidTestGroupDefinitionException(string.Join("\n", lines));
                }
                childrenfinitions.Add(childDefinition);

                curLine += blocLength - 1;
            }

            return new TestGroupDefinition(name, childrenfinitions);
        }

        public Script ParseLocalImport(string importStr, string executionDirPath)
        {
            Regex localImportRegex = new Regex(LocalImportPattern);
            Match localImportMatch = localImportRegex.Match(importStr);
            string path = localImportMatch.Groups[NamedGroups.Path].Value;
            if (executionDirPath != null)
            {
                path = Path.Combine(executionDirPath, path);
            }
            return ParseScript(path);
        }

        public Script ParseRepoImport(string importStr)
        {
            Regex repoImportRegex = new Regex(RepoImportPattern);
            Match repoImportMatch = repoImportRegex.Match(importStr);
            string repoName = repoImportMatch.Groups[NamedGroups.RepoName].Value;
            string repoFilePath = repoImportMatch.Groups[NamedGroups.Path].Value;
            return ParseRepoScript(repoName, repoFilePath);
        }

        public Script ParseImport(string importStr, string executionDirPath)
        {
            if (new Regex(LocalImportPattern).IsMatch(importStr))
            {
                return ParseLocalImport(importStr, executionDirPath);
            }
            return ParseRepoImport(importStr);
        }

        public Script ParseScript(string scriptContent, string executionDirPath)
        {
            List<string> lines = scriptContent.Split('\n').ToList();
            lines = lines.Select((line) => line.Replace("\t", new string(' ', 4))).ToList();

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
                else if (IsTestGroup(lines[curLine]))
                {
                    TestGroupDefinition testGroupDefinition = ParseTestGroupDefinition(lines.GetRange(curLine, blocLength));
                    script.TestDefinitions.Add(testGroupDefinition.Name, testGroupDefinition);
                }
                else if (IsTest(lines[curLine]))
                {
                    TestDefinition testDefinition = ParseTestDefinition(lines.GetRange(curLine, blocLength));
                    script.TestDefinitions.Add(testDefinition.Name, testDefinition);
                }
                else
                {
                    throw new UnrecognizedPatternExeception(lines[curLine]);
                }

                curLine += blocLength - 1;
            }

            return script;
        }

        public Script ParseScript(string localFilePath)
        {
            string executionDirPath = Path.GetDirectoryName(localFilePath);

            Script script;
            using (StreamReader fileReader = new StreamReader(localFilePath))
            {
                string scriptContent = fileReader.ReadToEnd();
                script = ParseScript(scriptContent, executionDirPath);
            }
            return script;
        }

        public Script ParseRepoScript(string repoName, string repoFilePath)
        {
            string githubUrl = $"https://raw.githubusercontent.com/{repoName}/master/{repoFilePath}";

            Script script;
            using (var webClient = new WebClient())
            {
                string scriptContent = webClient.DownloadString(githubUrl);
                script = ParseScript(scriptContent, null);
            }
            return script;
        }
    }
}
