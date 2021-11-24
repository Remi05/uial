using System;
using System.Collections.Generic;

namespace Uial.Definitions
{
    public class Script
    {
        public DefinitionScope RootScope { get; private set; } = new DefinitionScope();
        public Dictionary<string, ScenarioDefinition> ScenarioDefinitions { get; private set; } = new Dictionary<string, ScenarioDefinition>();
        public Dictionary<string, TestableDefinition> TestDefinitions { get; private set; } = new Dictionary<string, TestableDefinition>();
        public Dictionary<string, ModuleDefinition> ModuleDefinitions { get; private set; } = new Dictionary<string, ModuleDefinition>();

        public void AddScript(Script script)
        {
            foreach (string contextName in script.RootScope.ContextDefinitions.Keys)
            {
                if (RootScope.ContextDefinitions.ContainsKey(contextName))
                {
                    throw new Exception($"Context \"{contextName}\" already exists in the current scope.");
                }
                RootScope.ContextDefinitions.Add(contextName, script.RootScope.ContextDefinitions[contextName]);
            }

            foreach (string scenarioName in script.ScenarioDefinitions.Keys)
            {
                if (ScenarioDefinitions.ContainsKey(scenarioName))
                {
                    throw new Exception($"Scenario \"{scenarioName}\" already exists.");
                }
                ScenarioDefinitions.Add(scenarioName, script.ScenarioDefinitions[scenarioName]);
            }

            foreach (string moduleName in script.ModuleDefinitions.Keys)
            {
                if (!ModuleDefinitions.ContainsKey(moduleName))
                {
                    ModuleDefinitions.Add(moduleName, script.ModuleDefinitions[moduleName]);
                }
            }
        }
    }
}
