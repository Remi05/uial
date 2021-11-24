using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Definitions;
using Uial.Interactions;

namespace Uial.Scenarios
{
    public class ScenarioResolver : IScenarioResolver
    {
        private IBaseInteractionResolver BaseInteractionResolver { get; set; }

        public ScenarioResolver(IBaseInteractionResolver baseInteractionResolver)
        {
            BaseInteractionResolver = baseInteractionResolver;
        }

        public Scenario Resolve(ScenarioDefinition scenarioDefinition, IContext context, IInteractionProvider interactionProvider)
        {
            IEnumerable<IInteraction> interactions = scenarioDefinition.BaseInteractionDefinitions.Select((interactionDefinition) => BaseInteractionResolver.Resolve(interactionDefinition, context, interactionProvider, context?.Scope));
            return new Scenario(scenarioDefinition.ScenarioName, interactions);
        }
    }
}
