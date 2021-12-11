using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;

namespace Uial.Interactions
{
    public class GlobalInteractionProvider : IInteractionProvider
    {
        protected ISet<IInteractionProvider> InteractionProviders { get; set; } = new HashSet<IInteractionProvider>();

        public void AddProvider(IInteractionProvider interactionProvider)
        {
            InteractionProviders.Add(interactionProvider);
        }

        public void AddMultipleProviders(ICollection<IInteractionProvider> interactionProviders)
        {
            foreach (var interactionProvider in interactionProviders)
            {
                InteractionProviders.Add(interactionProvider);
            }
        }

        public bool IsInteractionAvailableForContext(string interactionName, IContext context)
        {
            return InteractionProviders.Any(interactionProvider => interactionProvider.IsInteractionAvailableForContext(interactionName, context));
        }

        public IInteraction GetInteractionByName(string interactionName, IEnumerable<object> paramValues, IContext context)
        {
            if (!IsInteractionAvailableForContext(interactionName, context))
            {
                throw new InteractionUnavailableException(interactionName);
            }
            IInteractionProvider interactionProvider = InteractionProviders.First(provider => provider.IsInteractionAvailableForContext(interactionName, context));
            return interactionProvider.GetInteractionByName(interactionName, paramValues, context);
        }
    }
}
