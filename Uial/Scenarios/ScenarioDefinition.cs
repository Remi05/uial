using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Interactions;

namespace Uial.Scenarios
{
    public class ScenarioDefinition : IScenarioDefinition
    {
        public string Name { get; private set; }
        private IEnumerable<IBaseInteractionDefinition> BaseInteractionDefinitions { get; set; }

        public ScenarioDefinition(string name, IEnumerable<IBaseInteractionDefinition> baseInteractionDefinitions)
        {
            if (name == null || baseInteractionDefinitions == null)
            {
                throw new ArgumentNullException(name == null ? nameof(name) : nameof(baseInteractionDefinitions));
            }
            Name = name;
            BaseInteractionDefinitions = baseInteractionDefinitions;
        }

        public Scenario Resolve(IContext context, IInteractionProvider interactionProvider)
        {
            IEnumerable<IInteraction> interactions = BaseInteractionDefinitions.Select((interactionDefinition) => interactionDefinition.Resolve(context, interactionProvider, context?.Scope));
            return new Scenario(Name, interactions);
        }
    }
}
