using System;
using System.Collections.Generic;

namespace Uial.Definitions
{
    public class ScenarioDefinition
    {
        public string ScenarioName { get; private set; }
        public IEnumerable<BaseInteractionDefinition> BaseInteractionDefinitions { get; private set; }

        public ScenarioDefinition(string scenarioName, IEnumerable<BaseInteractionDefinition> baseInteractionDefinitions)
        {
            if (scenarioName == null || baseInteractionDefinitions == null)
            {
                throw new ArgumentNullException(scenarioName == null ? nameof(scenarioName) : nameof(baseInteractionDefinitions));
            }
            ScenarioName = scenarioName;
            BaseInteractionDefinitions = baseInteractionDefinitions;
        }
    }
}
