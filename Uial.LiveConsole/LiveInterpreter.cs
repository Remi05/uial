using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UIAutomationClient;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Interactions;
using Uial.Modules;
using Uial.Parsing;
using Uial.Windows.Conditions;
using Uial.Windows.Contexts;
using Uial.Windows.Interactions;

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
        protected LiveInterpreterRuntime Runtime { get; set; }
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

            InitializeRuntime();
            InitializeCommands();
        }

        protected void InitializeRuntime()
        {
            var valueResolver = new ValueResolver();
            var conditionResolver = new ConditionResolver(valueResolver);

            Runtime = new LiveInterpreterRuntime();
            Runtime.AddContextProvider(new WindowsVisualContextProvider(conditionResolver));
            Runtime.AddInteractionProvider(new WindowsVisualInteractionProvider());
        }

        protected void InitializeCommands()
        {
            Commands.Add("cls",   (_) => ClearOutput());
            Commands.Add("clear", (_) => ClearOutput());
            Commands.Add("exit",  (_) => ShouldExit = true);
            Commands.Add("reset", (_) => InitializeRuntime());
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
                        Runtime.AddScript(Parser.ParseImport(line, null));
                    }
                    else if (Parser.IsModule(line))
                    {
                        ModuleDefinition moduleDefinition = Parser.ParseModuleDefinition(line);
                        Runtime.AddModule(moduleDefinition);
                    }
                    else if (Parser.IsContext(line))
                    {
                        DefinitionScope currentScope = new DefinitionScope(Runtime.RootScript.RootScope);
                        ContextDefinition contextDefinition = Parser.ParseContextDefinitionDeclaration(currentScope, line);
                        Runtime.RootContext.Scope.ContextDefinitions.Add(contextDefinition.Name, contextDefinition);
                    }
                    else if (Parser.IsBaseInteraction(line))
                    {
                        BaseInteractionDefinition baseInteractionDefinition = Parser.ParseBaseInteractionDefinition(line);
                        Runtime.RunInteraction(baseInteractionDefinition);
                    }
                    else if (Parser.IsCondition(line) || Parser.IsBaseContext(line))
                    {
                        ShowElement(line, TreeScope.TreeScope_Element);
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
            Runtime.AddScript(Parser.ParseRepoImport(repoStr));
            OutputStream.WriteLine($"Successfully imported \"{line}\" from UIAL catalog.");
        }

        protected void ShowAllElements(string line)
        {
            if (Parser.IsCondition(line))
            {
                ConditionDefinition conditionDefinition = Parser.ParseConditionDefinition(line);
                var conditionResolver = new ConditionResolver(new ValueResolver());
                var condition = conditionResolver.Resolve(conditionDefinition, null); // TODO: Fix value store
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
                ConditionDefinition conditionDefinition = Parser.ParseConditionDefinition(line);
                var conditionResolver = new ConditionResolver(new ValueResolver());
                var condition = conditionResolver.Resolve(conditionDefinition, null); // TODO: Fix value store
                var element = UIAutomation.GetRootElement().FindFirst(TreeScope.TreeScope_Subtree, condition);
                ShowElement(element, treeScope);
            }
            else if (Parser.IsBaseContext(line))
            {
                BaseContextDefinition baseContextDefinition = Parser.ParseBaseContextDefinition(line);
                var context = Runtime.ResolveBaseContext(baseContextDefinition) as IWindowsVisualContext;
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
