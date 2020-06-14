using System;
using System.Collections.Generic;
using System.Linq;
using ScenarioScripting.Contexts;
using ScenarioScripting.Interactions;
using ScenarioScripting.Scopes;

namespace ScenarioScripting.Scenarios
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

        public Scenario Resolve(IContext context)
        {
            IEnumerable<IInteraction> interactions = BaseInteractionDefinitions.Select((interactionDefinition) => interactionDefinition.Resolve(context, context?.Scope));
            return new Scenario(Name, interactions);
        }
    }
}
