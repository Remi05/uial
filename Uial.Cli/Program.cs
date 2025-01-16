using System;
using System.Collections.Generic;
using Uial.Contexts.Windows;
using Uial.Scenarios;
using Uial.Scopes;
using Uial.Testing;
using Uial.Parsing;
using Uial.Interactions;
using Uial.Modules;

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

            RunScenario(scriptFilePath, scenarioName, testName);
        }

        private static void RunScenario(string scriptFilePath, string scenarioName, string testName)
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
                var scope = new RuntimeScope(script.RootScope, new Dictionary<string, string>());
                var rootContext = new RootVisualContext(scope);

                var interactionProviders = new List<IInteractionProvider>()
                {
                    new Interactions.Core.CoreInteractionProvider(),
                    new Interactions.Windows.VisualInteractionProvider(),
                    new Snapshots.SnapshotsInteractionProvider(),
                };

                var importedInteractionProviders = GetImportedInteractionProviders(script);
                interactionProviders.AddRange(importedInteractionProviders);

                var interactionProvider = new GlobalInteractionProvider(interactionProviders);

                if (scenarioName != null)
                {
                    if (!script.ScenarioDefinitions.ContainsKey(scenarioName))
                    {
                        Console.WriteLine($"Scenario \"{scenarioName}\" could not be found in the given script.");
                        return;
                    }
                    Scenario scenario = script.ScenarioDefinitions[scenarioName].Resolve(rootContext, interactionProvider);
                    scenario.Do();
                }
                else if (testName != null)
                {
                    if (!script.TestDefinitions.ContainsKey(testName))
                    {
                        Console.WriteLine($"Test \"{testName}\" could not be found in the given script.");
                        return;
                    }
                    ITestable test = script.TestDefinitions[testName].Resolve(rootContext, interactionProvider);
                    ITestResults results = test.RunTest();
                    Console.WriteLine(results);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured while running the specified scenario:\n{e.Message}");
            }
        }

        private static List<IInteractionProvider> GetImportedInteractionProviders(Script script)
        {
            var interactionProviders = new List<IInteractionProvider>();
            var moduleProvider = new ModuleProvider();

            foreach (ModuleDefinition moduleDefinition in script.ModuleDefinitions.Values)
            {
                Module module = moduleProvider.GetModule(moduleDefinition);
                interactionProviders.AddRange(module.InteractionProviders);
            }

            return interactionProviders;
        }
    }
}
