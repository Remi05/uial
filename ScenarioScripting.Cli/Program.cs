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
            string filePath = "C:\\Users\\remi_\\Desktop\\launchteams.uial";

            ScriptParser parser = new ScriptParser();
            Script script = parser.ParseScript(filePath);

            Console.Write("Enter scenario: ");
            string scenarioName = Console.ReadLine();
            RuntimeScope scope = new RuntimeScope(script.RootScope, new Dictionary<string, string>());
            IContext rootContext = new RootContext(scope);
            Scenario scenario = script.ScenarioDefinitions[scenarioName].Resolve(rootContext);
            scenario.Do();
        }
    }
}
