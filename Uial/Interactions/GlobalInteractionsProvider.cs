using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Scopes;


namespace Uial.Interactions
{
    public class GlobalInteractionsProvider : IInteractionsProvider
    {
        protected IList<IInteractionsProvider> InteractionsProviders { get; set; }

        public GlobalInteractionsProvider(IList<IInteractionsProvider> interactionsProviders)
        {
            if (interactionsProviders == null)
            {
                throw new ArgumentNullException("interactionsProviders");
            }
            InteractionsProviders = interactionsProviders;
        }

        public bool IsKnownInteraction(string interactionName)
        {
            return InteractionsProviders.Any(interactionProvider => interactionProvider.IsKnownInteraction(interactionName));
        }

        public IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues)
        {
            IInteractionsProvider interactionProvider = InteractionsProviders.First(provider => provider.IsKnownInteraction(interactionName));
            return interactionProvider?.GetInteractionByName(context, scope, interactionName, paramValues);
        }
    }
}
