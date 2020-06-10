using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using ScenarioScripting;
using ScenarioScripting.Contexts;
using ScenarioScripting.Interactions;

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
            public const string Name = "name";
            public const string RootElementCondition = "rootCondition";
            public const string UniqueCondition = "uniqueCondition";
            public const string Params = "params";
            public const string Context = "context";
            public const string ControlCondition = "controlCondition";
            public const string ControlType = "controlType";
            public const string Interaction = "interaction";
            public const string Property = "property";
            public const string Value = "value";
        }

        const string PropertyConditionPattern = "(?<property>[A-z]+)\\s*=\\s*\"(?<value>[^\"]+)\"";
        const string SingleConditionPattern = "[A-z]+\\s*=\\s*\"[^\"]+\"";
        const string ConditionPattern = SingleConditionPattern + "(?:\\s*,\\s*" + SingleConditionPattern + ")*";

        const string ContextPattern = BlocIdentifiers.Context + "\\s+(?<name>[A-z]+)\\s+" + "(?:\\[\\s*(?<rootCondition>" + ConditionPattern + ")\\s*\\])?\\s*?(?:\\s+\\{\\s*(?<uniqueCondition>" + ConditionPattern + ")\\s*\\})?\\s*:";
        const string InteractionPattern = BlocIdentifiers.Interaction + "\\s+(?<name>[A-z]+)\\s*:";
        const string ScenarioPattern = BlocIdentifiers.Scenario + "\\s+(?<name>[A-z]+)\\s*:";
        const string BaseInteractionPattern = "(?<context>[A-z]+(?:::[A-z]+)*)?(?:::(?<controlType>[A-z]+)\\[(?<controlCondition>" + SingleConditionPattern + ")\\])?::(?<interaction>[A-z]+)\\(\\)";

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

        Condition ParseCondition(string conditionStr)
        {
            Regex conditionRegex = new Regex(PropertyConditionPattern);
            MatchCollection matches = conditionRegex.Matches(conditionStr);
            Condition condition = null;
            foreach (Match match in matches)
            {
                AutomationProperty property = Controls.GetPropertyByName(match.Groups[NamedGroups.Property].Value);
                object value = Controls.GetPropertyValue(property, match.Groups[NamedGroups.Value].Value);
                Condition propertyCondition =  new PropertyCondition(property, value);
                condition = condition == null ? propertyCondition : new AndCondition(condition, propertyCondition);
            }
            return condition;
        }

        IContext ParseContextDeclaration(IContext parentContext, string line)
        {
            line = line.Trim();
            Regex contextRegex = new Regex(ContextPattern);
            Match regexMatch = contextRegex.Match(line);
            if (!regexMatch.Success)
            {
                return null;
            }

            string name = regexMatch.Groups[NamedGroups.Name].Value;

            Condition rootElementCondition = null;
            if (regexMatch.Groups[NamedGroups.RootElementCondition].Success)
            {
                string rootElementConditionStr = regexMatch.Groups[NamedGroups.RootElementCondition].Value;
                rootElementCondition = ParseCondition(rootElementConditionStr);
            }

            Condition uniqueCondition = null;
            if (regexMatch.Groups[NamedGroups.UniqueCondition].Success)
            {
                string uniqueConditionStr = regexMatch.Groups[NamedGroups.UniqueCondition].Value;
                uniqueCondition = ParseCondition(uniqueConditionStr);
            }

            return new Context(parentContext, name, rootElementCondition, uniqueCondition);
        }

        IContext ParseContext(IContext currentContext, List<string> lines)
        {
            List<IContext> childrenContexts = new List<IContext>();
            List<IInteraction> interactionDefinitions = new List<IInteraction>();
            IContext context = ParseContextDeclaration(currentContext, lines[0]);

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
                    IContext childContext = ParseContext(context, childBloc);
                    childrenContexts.Add(childContext);
                }
                else if (IsInteraction(lines[curLine]))
                {
                    IInteraction interaction = ParseInteraction(context, childBloc);
                    interactionDefinitions.Add(interaction);
                }
                else
                {
                    throw new Exception(); // TODO: Specify exception
                }
                curLine = blocEnd;
            }

            return context;
        }

        IInteraction ParseInteraction(IContext currentContext, List<string> lines)
        {
            string declarationLine = lines[0].Trim();
            Regex interactionRegex = new Regex(InteractionPattern);
            Match regexMatch = interactionRegex.Match(declarationLine);
            string name = regexMatch.Groups[NamedGroups.Name].Value;

            List<IInteraction> childrenInteractions = new List<IInteraction>();
            for (int curLine = 1; curLine < lines.Count; ++curLine)
            {
                childrenInteractions.Add(ParseBaseInteraction(currentContext, lines[curLine]));
            }

            return new CompositeInteraction(currentContext, name, childrenInteractions);
        }

        IContext GetBaseInteractionContext(IContext currentContext, string contextName)
        {
            if (!currentContext.ChildrenContexts.ContainsKey(contextName))
            {
                throw new ContextNotFoundException(contextName, currentContext);
            }
            return currentContext.ChildrenContexts[contextName];
        }

        IInteraction ParseBaseInteraction(IContext currentContext, string line)
        {
            Regex baseInterationRegex = new Regex(BaseInteractionPattern);
            Match regexMatch = baseInterationRegex.Match(line);

            IContext context = currentContext;

            if (regexMatch.Groups[NamedGroups.Context].Success)
            {
                string composedContextStr = regexMatch.Groups[NamedGroups.Context].Value;
                string[] contextsStrings = composedContextStr.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);

                context = GetBaseInteractionContext(currentContext, contextsStrings[0]);
                for (int i = 1; i < contextsStrings.Length; ++i)
                {
                    string contextName = contextsStrings[i];
                    if (!context.ChildrenContexts.ContainsKey(contextName))
                    {
                        throw new ContextNotFoundException(contextName, context);
                    }
                    context = context.ChildrenContexts[contextName];
                }
            }

            if (regexMatch.Groups[NamedGroups.ControlType].Success && regexMatch.Groups[NamedGroups.ControlCondition].Success)
            {
                string controlTypeStr = regexMatch.Groups[NamedGroups.ControlType].Value;
                Condition uniqueCondition = ParseCondition(regexMatch.Groups[NamedGroups.ControlCondition].Value);
                context = Controls.GetContextFromControl(context, controlTypeStr, uniqueCondition);
            }

            string interactionName = regexMatch.Groups[NamedGroups.Interaction].Value;
            if (context.Interactions.ContainsKey(interactionName))
            {
                return context.Interactions[interactionName];
            }

            IInteraction interaction = Interactions.GetBasicInteractionByName(interactionName);
            if (interaction == null)
            {
                throw new InteractionUnavailableException();
            }

            return interaction;
        }

        Scenario ParseScenario(IContext currentContext, List<string> lines)
        {
            string declarationLine = lines[0].Trim();
            Regex scenarioRegex = new Regex(ScenarioPattern);
            Match regexMatch = scenarioRegex.Match(declarationLine);
            string name = regexMatch.Groups[NamedGroups.Name].Value;

            List<IInteraction> childrenInteractions = new List<IInteraction>();
            for (int i = 1; i < lines.Count; ++i)
            {
                childrenInteractions.Add(ParseBaseInteraction(currentContext, lines[0]));
            }

            return new Scenario(currentContext, name, childrenInteractions);
        }

        Script ParseScript(List<string> lines)
        {
            Script script = new Script();
            IContext rootContext = new RootContext();

            for (int curLine = 0; curLine < lines.Count; ++curLine)
            {
                if (string.IsNullOrWhiteSpace(lines[curLine]))
                {
                    continue;
                }

                int blocEnd = FindBlocEnd(lines, curLine);
                if (IsScenario(lines[curLine]))
                {
                    Scenario scenario = ParseScenario(rootContext, lines.GetRange(curLine, blocEnd));
                    script.Scenarios.Add(scenario.Name, scenario);
                }
                else if (IsContext(lines[curLine]))
                {
                    IContext context = ParseContext(rootContext, lines.GetRange(curLine, blocEnd));
                    script.Contexts.Add(context.Name, context);
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
