using System.Collections.Generic;
using Uial.Contexts;
using Uial.Values;

namespace Uial.Interactions.Core
{
    public class CoreInteractionProvider : IInteractionProvider
    {
        protected delegate IInteraction InteractionFactory(IContext context, IEnumerable<object> paramValues, IReferenceValueStore referenceValueStore);

        protected IDictionary<string, InteractionFactory> KnownInteractions = new Dictionary<string, InteractionFactory>()
        {
            { IsAvailable.Key,         (context, paramValues, referenceValueStore) => IsAvailable.FromRuntimeValues(context, paramValues, referenceValueStore) },
            { Wait.Key,                (context, paramValues, _) => Wait.FromRuntimeValues(paramValues) },
            { WaitUntilAvailable.Key,  (context, paramValues, _) => WaitUntilAvailable.FromRuntimeValues(context, paramValues) },
        };

        public bool IsInteractionAvailableForContext(string interactionName, IContext context)
        {
            return KnownInteractions.ContainsKey(interactionName);
        }

        public IInteraction GetInteractionByName(string interactionName, IEnumerable<object> paramValues, IContext context)
        {
            if (!IsInteractionAvailableForContext(interactionName, context))
            {
                throw new InteractionUnavailableException(interactionName);
            }
            return KnownInteractions[interactionName](context, paramValues, context?.Scope?.ReferenceValueStore); // TODO: Review value store, should be of interaction
        }
    }
}

