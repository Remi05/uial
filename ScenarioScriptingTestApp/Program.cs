using System;
using System.IO;
using ScenarioScripting;
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
            script.Scenarios[scenarioName].Do(null);
        }
    }
}
