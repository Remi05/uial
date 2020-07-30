using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Automation;
using Uial.Conditions;
using Uial.Contexts;
using Uial.Interactions;
using Uial.Parsing;
using Uial.Scopes;

namespace Uial.LiveConsole
{
    public class LiveInterpreter
    {
        protected TextReader InputStream { get; set; }
        protected TextWriter OutputStream { get; set; }
        protected ScriptParser Parser { get; set; } = new ScriptParser();

        public LiveInterpreter(TextReader inputStream, TextWriter outputStream)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException("inputStream");
            }
            InputStream = inputStream;
            OutputStream = outputStream;
        }

        public void Run()
        {
            var script = new Script();
            var rootScope = new RuntimeScope(script.RootScope, new Dictionary<string, string>());
            var rootContext = new RootContext(rootScope);

            while (true)
            {
                try
                {
                    string line = InputStream.ReadLine();
                    if (Parser.IsImport(line))
                    {
                        script.AddScript(Parser.ParseRepoImport(line));
                    }
                    else if (Parser.IsContext(line))
                    {
                        DefinitionScope currentScope = new DefinitionScope(script.RootScope);
                        IContextDefinition contextDefinition = Parser.ParseContextDefinitionDeclaration(currentScope, line);
                        script.RootScope.ContextDefinitions.Add(contextDefinition.Name, contextDefinition);
                    }
                    else if (Parser.IsBaseInteraction(line))
                    {
                        IBaseInteractionDefinition baseInteractionDefinition = Parser.ParseBaseInteractionDefinition(line);
                        IInteraction interaction = baseInteractionDefinition.Resolve(rootContext, rootScope);
                        interaction.Do();
                    }
                    else if (Parser.IsCondition(line))
                    {
                        IConditionDefinition conditionDefinition = Parser.ParseConditionDefinition(line);
                        Condition condition = conditionDefinition.Resolve(rootScope);
                        AutomationElement element = AutomationElement.RootElement.FindFirst(TreeScope.Subtree, condition);
                        OutputElementInfo(element);
                    }
                    else if (Parser.IsBaseContext(line))
                    {
                        IBaseContextDefinition baseContextDefinition = Parser.ParseBaseContextDefinition(line);
                        IContext context = baseContextDefinition.Resolve(rootContext, rootScope);
                        OutputElementInfo(context.RootElement);
                    }
                }
                catch (Exception e)
                {
                    OutputStream.WriteLine(e.Message + "\n");
                }
            }
        }

        protected void OutputElementInfo(AutomationElement automationElement)
        {
            string elementInfo = automationElement == null ? "Element not found.\n" : $"[{Helper.GetConditionFromElement(automationElement)}]\n";
            OutputStream.WriteLine(elementInfo);
        }
    }
}
