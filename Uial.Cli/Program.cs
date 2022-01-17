using System;
using System.Collections.Generic;
using Uial.DataModels;
using Uial.Parsing;
using Uial.Scopes;
using Uial.Testing;
using Uial.Values;

namespace Uial.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            string scriptFilePath;
            string scenarioName = null;
            string testName = null;

            if (args.Length == 0)
            {
                Console.WriteLine("Script file path: ");
                scriptFilePath = Console.ReadLine();
            }
            else
            {
                scriptFilePath = args[0];
            }

            if (args.Length < 2)
            {
                Console.WriteLine("Scenario name: ");
                scenarioName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(scenarioName))
                {
                    Console.WriteLine("Test name: ");
                    testName = Console.ReadLine();
                }
            }
            else
            {
                string flag = args[1].Split(':')[0];
                string value = args[1].Split(':')[1];
                if (flag == "/scenario")
                {
                    scenarioName = value;
                }
                else if (flag == "/test")
                {
                    testName = value;
                }
                else
                {
                    Console.WriteLine($"Invalid flag: {flag}");
                    return;
                }
            }

            RunScript(scriptFilePath, scenarioName, testName);
        }

        private static void RunScript(string scriptFilePath, string scenarioName, string testName)
        {
            var parser = new ScriptParser();
            Script script;

            try
            {
                script = parser.ParseScript(scriptFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while parsing the given script:\n{e.Message}");
                return;
            }

            try
            {
                var scope = new RuntimeScope(script.RootScope, new ReferenceValueStore());
                var rootContext = new Windows.Contexts.RootVisualContext(scope);

                var runtime = new UialRuntime(rootContext);
                runtime.AddContextProvider(new Windows.Contexts.WindowsVisualContextProvider());
                runtime.AddInteractionProvider(new Interactions.Core.CoreInteractionProvider());
                runtime.AddInteractionProvider(new Windows.Interactions.VisualInteractionProvider());

                if (scenarioName != null)
                {
                    runtime.RunScenario(scenarioName);
                }
                else if (testName != null)
                {
                    ITestResults results = runtime.RunTest(testName);
                    Console.WriteLine(results);                 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured while running the specified scenario/test:\n{e.Message}");
            }
        }
    }
}
