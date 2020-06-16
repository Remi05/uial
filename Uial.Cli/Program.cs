using System;
using System.Collections.Generic;
using Uial.Contexts;
using Uial.Scenarios;
using Uial.Scopes;
using Uial.Parsing;

namespace Uial.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            string scriptFilePath;
            string scenarioName;

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
            }
            else
            {
                scenarioName = args[1];
            }

            RunScenario(scriptFilePath, scenarioName);
        }

        private static void RunScenario(string scriptFilePath, string scenarioName)
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
            
              
            if (!script.ScenarioDefinitions.ContainsKey(scenarioName))
            {
                Console.WriteLine($"Scenario \"{scenarioName}\" could not be found in the given script.");
                return;
            }

            try
            {
                var scope = new RuntimeScope(script.RootScope, new Dictionary<string, string>());
                var rootContext = new RootContext(scope);
                Scenario scenario = script.ScenarioDefinitions[scenarioName].Resolve(rootContext);
                scenario.Do();
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured while running the specified scenario:\n{e.Message}");
            }
        }
    }
}
