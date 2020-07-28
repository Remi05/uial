using System.Collections.Generic;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions.Core
{
    public class CoreInteractionsProvider : IInteractionsProvider
    {
        protected ISet<string> KnownInteractions = new HashSet<string>()
        {
            IsAvailable.Key,
            Wait.Key,
            WaitUntilAvailable.Key,
        };

        public bool IsKnownInteraction(string interactionName)
        {
            return KnownInteractions.Contains(interactionName);
        }

        public IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues)
        {
            switch (interactionName)
            {
                case IsAvailable.Key:
                    return IsAvailable.FromRuntimeValues(context, scope, paramValues);
                case Wait.Key:
                    return Wait.FromRuntimeValues(paramValues);
                case WaitUntilAvailable.Key:
                    return WaitUntilAvailable.FromRuntimeValues(context, paramValues);
                default:
                    throw new InteractionUnavailableException(interactionName);
            }
        }
    }
}

