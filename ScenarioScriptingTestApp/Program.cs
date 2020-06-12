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
            string filePath = "C:\\Users\\remi_\\Desktop\\launchteams.txt";
            StreamReader fileReader = new StreamReader(filePath);
            ScriptParser parser = new ScriptParser();
            Script script = parser.ParseScript(fileReader);
            fileReader.Close();

            Console.Write("Enter scenario: ");
            string scenarioName = Console.ReadLine();
            RuntimeScope scope = new RuntimeScope(script.RootScope, new Dictionary<string, object>());
            IContext rootContext = new RootContext(scope);
            Scenario scenario = script.ScenarioDefinitions[scenarioName].Resolve(rootContext);
            scenario.Do();
        }
    }
}
