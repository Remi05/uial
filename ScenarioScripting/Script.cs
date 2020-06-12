using System.Collections.Generic;
using ScenarioScripting.Scenarios;
using ScenarioScripting.Scopes;

namespace ScenarioScripting
{
    public class Script
    {
        public Scope RootScope { get; private set; } = new Scope();
        public Dictionary<string, IScenarioDefinition> ScenarioDefinitions { get; private set; } = new Dictionary<string, IScenarioDefinition>();
    }
}
