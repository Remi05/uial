using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UIAutomationClient;
using Uial.Conditions;
using Uial.Contexts;
using Uial.Contexts.Windows;
using Uial.Interactions;
using Uial.Modules;
using Uial.Parsing;
using Uial.Scopes;

namespace Uial.LiveConsole
{
    public class LiveInterpreter
    {
        protected delegate void Command(string line);

        private IUIAutomation UIAutomation { get; set; } = new CUIAutomation();
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
            Commands.Add("root",  (_) => ShowElement(UIAutomation.GetRootElement(), TreeScope.TreeScope_Element));
            Commands.Add("ancestors",   (line) => ShowElement(line, TreeScope.TreeScope_Ancestors));
            Commands.Add("children",    (line) => ShowElement(line, TreeScope.TreeScope_Children));
            Commands.Add("descendants", (line) => ShowElement(line, TreeScope.TreeScope_Descendants));
            Commands.Add("element",     (line) => ShowElement(line, TreeScope.TreeScope_Element));
            Commands.Add("parent",      (line) => ShowElement(line, TreeScope.TreeScope_Parent));
            Commands.Add("subtree",     (line) => ShowElement(line, TreeScope.TreeScope_Subtree));
            Commands.Add("frompoint",   (line) => FromPoint(line));
            Commands.Add("import-catalog", ImportScriptFromCatalog);
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
                        // Remove the command from the line for further processing.
                        if (splits.Length > 1)
                        {
                            line = string.Join(' ', splits.Skip(1));
                        }

                        Commands[commandStr](line);
                        continue;
                    }
   
                    if (Parser.IsImport(line))
                    {
                        ExecutionContext.Script.AddScript(Parser.ParseImport(line, null));
                    }
                    else if (Parser.IsModule(line))
                    {
                        ModuleDefinition moduleDefinition = Parser.ParseModuleDefinition(line);
                        ExecutionContext.AddModule(moduleDefinition);
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
                        var condition = conditionDefinition.Resolve(ExecutionContext.RootScope);
                        var element = UIAutomation.GetRootElement().FindFirst(TreeScope.TreeScope_Subtree, condition);
                        ShowElement(element, TreeScope.TreeScope_Element);
                    }
                    else if (Parser.IsBaseContext(line))
                    {
                        IBaseContextDefinition baseContextDefinition = Parser.ParseBaseContextDefinition(line);
                        IWindowsVisualContext context = baseContextDefinition.Resolve(ExecutionContext.RootContext, ExecutionContext.RootScope) as IWindowsVisualContext;
                        ShowElement(context.RootElement, TreeScope.TreeScope_Element);
                    }
                }
                catch (Exception e)
                {
                    OutputStream.WriteLine(e.Message + "\n");
                }
            }
        }

        protected void ImportScriptFromCatalog(string line)
        {
            string repoStr = $"import 'github:Remi05/uial_catalog/{line}'";
            ExecutionContext.Script.AddScript(Parser.ParseRepoImport(repoStr));
        }

        protected void ShowAllElements(string line)
        {
            if (Parser.IsCondition(line))
            {
                IConditionDefinition conditionDefinition = Parser.ParseConditionDefinition(line);
                var condition = conditionDefinition.Resolve(ExecutionContext.RootScope);
                var elements = UIAutomation.GetRootElement().FindAll(TreeScope.TreeScope_Subtree, condition);
                if (elements.Length > 0)
                {
                    for (int i = 0; i < elements.Length; ++i)
                    {
                        string elementRepresentation = VisualTreeSerializer.GetElementRepresentation(elements.GetElement(i));
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
                var condition = conditionDefinition.Resolve(ExecutionContext.RootScope);
                var element = UIAutomation.GetRootElement().FindFirst(TreeScope.TreeScope_Subtree, condition);
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

        protected void ShowElement(IUIAutomationElement element, TreeScope treeScope)
        {
            if (element == null)
            {
                OutputStream.WriteLine("Element not found.\n");
                return;
            }
            string elementInfo = VisualTreeSerializer.GetVisualTreeRepresentation(element, treeScope);    
            OutputStream.WriteLine(elementInfo);
        }

        protected void FromPoint(string line)
        {
            string[] splits = line.Split(',');
            var point = new tagPOINT() { x = int.Parse(splits[0]), y = int.Parse(splits[1]) };
            var element = UIAutomation.ElementFromPoint(point);
            ShowElement(element, TreeScope.TreeScope_Element);
        }
    }
}
