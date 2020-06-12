using System;
using System.Collections.Generic;
using System.IO;
using ScenarioScripting;
using ScenarioScripting.Contexts;
using ScenarioScripting.Scenarios;
using ScenarioScripting.Scopes;
using ScenarioScriptParser;

namespace ScenarioScriptingTestApp
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
