using System.Collections.Generic;
using ScenarioScripting.Scenarios;
using ScenarioScripting.Scopes;

namespace ScenarioScripting
{
    public class Script
    {
        public DefinitionScope RootScope { get; private set; } = new DefinitionScope();
        public Dictionary<string, IScenarioDefinition> ScenarioDefinitions { get; private set; } = new Dictionary<string, IScenarioDefinition>();
    }
}
