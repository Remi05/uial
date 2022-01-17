using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Interactions;
using Uial.Values;

namespace Uial.Scenarios
{
    public class ScenarioResolver : IScenarioResolver
    {
        private IBaseInteractionResolver BaseInteractionResolver { get; set; }

        public ScenarioResolver(IBaseInteractionResolver baseInteractionResolver)
        {
            BaseInteractionResolver = baseInteractionResolver;
        }

        public Scenario Resolve(ScenarioDefinition scenarioDefinition, IContext context, IReferenceValueStore referenceValueStore)
        {
            IEnumerable<IInteraction> interactions = scenarioDefinition.BaseInteractionDefinitions.Select((interactionDefinition) => BaseInteractionResolver.Resolve(interactionDefinition, context, referenceValueStore));
            return new Scenario(scenarioDefinition.Name, interactions);
        }
    }
}
