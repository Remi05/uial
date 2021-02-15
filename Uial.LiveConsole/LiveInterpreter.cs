using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Automation;
using Uial.Conditions;
using Uial.Contexts;
using Uial.Contexts.Windows;
using Uial.Interactions;
using Uial.Parsing;
using Uial.Scopes;

namespace Uial.LiveConsole
{
    public class LiveInterpreter
    {
        protected delegate void Command(string line);

        protected TextReader InputStream { get; set; }
        protected TextWriter OutputStream { get; set; }
        protected ScriptParser Parser { get; set; } = new ScriptParser();
        protected VisualTreeSerializer VisualTreeSerializer = new VisualTreeSerializer();
        protected ExecutionContext ExecutionContext { get; set; } = new ExecutionContext();
        protected IDictionary<string, Command> Commands { get; set; } = new Dictionary<string, Command>();
        protected Action ClearOutput { get; set; }
        protected bool ShouldExit { get; set; } = false;

        public LiveInterpreter(TextReader inputStream, TextWriter outputStream, Action clearOutput)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException(nameof(inputStream));
            }
            InputStream = inputStream;
            OutputStream = outputStream;
            ClearOutput = clearOutput;
            InitializeCommands();
        }

        protected void InitializeCommands()
        {
            Commands.Add("cls",   (_) => ClearOutput());
            Commands.Add("clear", (_) => ClearOutput());
            Commands.Add("exit",  (_) => ShouldExit = true);
            Commands.Add("reset", (_) => ExecutionContext = new ExecutionContext());
            Commands.Add("root",  (_) => ShowElement(AutomationElement.RootElement, TreeScope.Element));
            Commands.Add("ancestors",   (line) => ShowElement(line, TreeScope.Ancestors));
            Commands.Add("children",    (line) => ShowElement(line, TreeScope.Children));
            Commands.Add("descendants", (line) => ShowElement(line, TreeScope.Descendants));
            Commands.Add("element",     (line) => ShowElement(line, TreeScope.Element));
            Commands.Add("parent",      (line) => ShowElement(line, TreeScope.Parent));
            Commands.Add("subtree",     (line) => ShowElement(line, TreeScope.Subtree));
            Commands.Add("import", ImportScript);
            Commands.Add("all", ShowAllElements);
        }

        public void Run()
        {  
            while (!ShouldExit)
            {
                try
                {
                    string line = InputStream.ReadLine();
                    string[] splits = line.Split(' ');
                    string commandStr = splits[0];              
                    
                    if (Commands.ContainsKey(commandStr))
                    {
                        if (splits.Length > 1)
                        {
                            line = string.Concat(splits.Skip(1));
                        }

                        Commands[commandStr](line);
                        continue;
                    }
   
                    if (Parser.IsImport(line))
                    {
                        ExecutionContext.Script.AddScript(Parser.ParseRepoImport(line));
                    }
                    else if (Parser.IsContext(line))
                    {
                        DefinitionScope currentScope = new DefinitionScope(ExecutionContext.Script.RootScope);
                        IContextDefinition contextDefinition = Parser.ParseContextDefinitionDeclaration(currentScope, line);
                        ExecutionContext.RootScope.ContextDefinitions.Add(contextDefinition.Name, contextDefinition);
                    }
                    else if (Parser.IsBaseInteraction(line))
                    {
                        IBaseInteractionDefinition baseInteractionDefinition = Parser.ParseBaseInteractionDefinition(line);
                        IInteraction interaction = baseInteractionDefinition.Resolve(ExecutionContext.RootContext, ExecutionContext.InteractionProvider, ExecutionContext.RootScope);
                        interaction.Do();
                    }
                    else if (Parser.IsCondition(line))
                    {
                        IConditionDefinition conditionDefinition = Parser.ParseConditionDefinition(line);
                        Condition condition = conditionDefinition.Resolve(ExecutionContext.RootScope);
                        AutomationElement element = AutomationElement.RootElement.FindFirst(TreeScope.Subtree, condition);
                        ShowElement(element, TreeScope.Element);
                    }
                    else if (Parser.IsBaseContext(line))
                    {
                        IBaseContextDefinition baseContextDefinition = Parser.ParseBaseContextDefinition(line);
                        IWindowsVisualContext context = baseContextDefinition.Resolve(ExecutionContext.RootContext, ExecutionContext.RootScope) as IWindowsVisualContext;
                        ShowElement(context.RootElement, TreeScope.Element);
                    }
                }
                catch (Exception e)
                {
                    OutputStream.WriteLine(e.Message + "\n");
                }
            }
        }

        protected void ImportScript(string line)
        {
            string repoStr = $"import 'github:Remi05/uial/samples/contexts/{line}.uial'";
            ExecutionContext.Script.AddScript(Parser.ParseRepoImport(repoStr));
        }

        protected void ShowAllElements(string line)
        {
            if (Parser.IsCondition(line))
            {
                IConditionDefinition conditionDefinition = Parser.ParseConditionDefinition(line);
                Condition condition = conditionDefinition.Resolve(ExecutionContext.RootScope);
                var elements = AutomationElement.RootElement.FindAll(TreeScope.Subtree, condition);
                if (elements.Count > 0)
                {
                    foreach (AutomationElement element in elements)
                    {
                        string elementRepresentation = VisualTreeSerializer.GetElementRepresentation(element);
                        OutputStream.Write(elementRepresentation);
                    }
                    Console.WriteLine();
                }
                else
                {
                    OutputStream.WriteLine($"No elements found matching condition {line}.");
                }
            }
            else
            {
                OutputStream.WriteLine($"\"{line}\" is not recognized as a valid condition.");
            }
        }

        protected void ShowElement(string line, TreeScope treeScope)
        {
            if (Parser.IsCondition(line))
            {
                IConditionDefinition conditionDefinition = Parser.ParseConditionDefinition(line);
                Condition condition = conditionDefinition.Resolve(ExecutionContext.RootScope);
                AutomationElement element = AutomationElement.RootElement.FindFirst(TreeScope.Subtree, condition);
                ShowElement(element, treeScope);
            }
            else if (Parser.IsBaseContext(line))
            {
                IBaseContextDefinition baseContextDefinition = Parser.ParseBaseContextDefinition(line);
                IWindowsVisualContext context = baseContextDefinition.Resolve(ExecutionContext.RootContext, ExecutionContext.RootScope) as IWindowsVisualContext;
                ShowElement(context.RootElement, treeScope);
            }
            else
            {
                OutputStream.WriteLine($"\"{line}\" is not recognized as a valid condition or context.");
            }
        }

        protected void ShowElement(AutomationElement element, TreeScope treeScope)
        {
            if (element == null)
            {
                OutputStream.WriteLine("Element not found.\n");
                return;
            }
            string elementInfo = VisualTreeSerializer.GetVisualTreeRepresentation(element, treeScope);    
            OutputStream.WriteLine(elementInfo);
        }
    }
}
