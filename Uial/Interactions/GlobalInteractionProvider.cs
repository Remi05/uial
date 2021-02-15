using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Scopes;


namespace Uial.Interactions
{
    public class GlobalInteractionProvider : IInteractionProvider
    {
        protected IList<IInteractionProvider> InteractionProviders { get; set; }

        public GlobalInteractionProvider(IList<IInteractionProvider> interactionProviders)
        {
            if (interactionProviders == null)
            {
                throw new ArgumentNullException(nameof(interactionProviders));
            }
            InteractionProviders = interactionProviders;
        }

        public bool IsKnownInteraction(string interactionName)
        {
            return InteractionProviders.Any(interactionProvider => interactionProvider.IsKnownInteraction(interactionName));
        }

        public IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues)
        {
            if (!IsKnownInteraction(interactionName))
            {
                throw new InteractionUnavailableException(interactionName);
            }
            IInteractionProvider interactionProvider = InteractionProviders.First(provider => provider.IsKnownInteraction(interactionName));
            return interactionProvider?.GetInteractionByName(context, scope, interactionName, paramValues);
        }
    }
}
