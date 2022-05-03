using System;
using System.Collections.Generic;

namespace Uial.DataModels
{
    public class ScenarioDefinition : InteractionDefinition
    {
        public ScenarioDefinition(string name, IEnumerable<BaseInteractionDefinition> baseInteractionDefinitions)
            : base(null, name, null, baseInteractionDefinitions)
        {
        }
    }
}
