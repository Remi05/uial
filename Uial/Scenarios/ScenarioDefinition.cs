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
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (baseInteractionDefinitions == null)
            {
                throw new ArgumentNullException("baseInteractionDefinitions");
            }
            Name = name;
            BaseInteractionDefinitions = baseInteractionDefinitions;
        }

        public Scenario Resolve(IContext context, IInteractionsProvider interactionsProvider)
        {
            IEnumerable<IInteraction> interactions = BaseInteractionDefinitions.Select((interactionDefinition) => interactionDefinition.Resolve(context, interactionsProvider, context?.Scope));
            return new Scenario(Name, interactions);
        }
    }
}
