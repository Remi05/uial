using System;
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
            if (scenarioDefinition == null)
            {
                throw new ArgumentNullException(nameof(scenarioDefinition));
            }

            var interactions = new List<IInteraction>();
            if (scenarioDefinition.BaseInteractionDefinitions != null && scenarioDefinition.BaseInteractionDefinitions.Count() > 0)
            {
                foreach (BaseInteractionDefinition baseInteractionDefinition in scenarioDefinition.BaseInteractionDefinitions)
                {
                    IInteraction interaction = BaseInteractionResolver.Resolve(baseInteractionDefinition, context, referenceValueStore);
                    interactions.Add(interaction);
                }
            }

            return new Scenario(scenarioDefinition.Name, interactions);
        }
    }
}
