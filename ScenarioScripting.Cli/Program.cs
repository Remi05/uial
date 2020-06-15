using System;
using System.Collections.Generic;
using ScenarioScripting.Contexts;
using ScenarioScripting.Scenarios;
using ScenarioScripting.Scopes;
using ScenarioScripting.Parser;

namespace ScenarioScripting.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Expected two arguments: <script_file_path> <scenario_name>");
                return;
            }

            string scriptFilePath = args[0];
            string scenarioName = args[1];

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
