using System.Collections.Generic;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions.Core
{
    public class CoreInteractionProvider : IInteractionProvider
    {
        protected delegate IInteraction InteractionFactory(IContext context, RuntimeScope scope, IEnumerable<string> paramValues);

        protected IDictionary<string, InteractionFactory> KnownInteractions = new Dictionary<string, InteractionFactory>()
        {
            { IsAvailable.Key,         (context, scope, paramValues) => IsAvailable.FromRuntimeValues(context, scope, paramValues) },
            { Wait.Key,                (_, _, paramValues) => Wait.FromRuntimeValues(paramValues) },
            { WaitUntilAvailable.Key,  (context, _, paramValues) => WaitUntilAvailable.FromRuntimeValues(context, paramValues) },
        };

        public bool IsKnownInteraction(string interactionName)
        {
            return KnownInteractions.ContainsKey(interactionName);
        }

        public IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues)
        {
            if (!IsKnownInteraction(interactionName))
            {
                throw new InteractionUnavailableException(interactionName);
            }
            return KnownInteractions[interactionName](context, scope, paramValues);
        }
    }
}

