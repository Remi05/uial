using System;
using System.Collections.Generic;
using System.Linq;
using ScenarioScripting.Scenarios;
using ScenarioScripting.Scopes;

namespace ScenarioScripting
{
    public class Script
    {
        public DefinitionScope RootScope { get; private set; } = new DefinitionScope();
        public Dictionary<string, IScenarioDefinition> ScenarioDefinitions { get; private set; } = new Dictionary<string, IScenarioDefinition>();
        
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
        }
    }
}
