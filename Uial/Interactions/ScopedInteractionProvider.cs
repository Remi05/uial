using System;
using System.Collections.Generic;
using Uial.Contexts;
using Uial.DataModels;

namespace Uial.Interactions
{
    public class ScopedInteractionProvider : IInteractionProvider
    {
        protected IInteractionResolver InteractionResolver { get; set; }

        public ScopedInteractionProvider(IInteractionResolver interactionResolver)
        {
            if (interactionResolver == null)
            {
                throw new ArgumentNullException(nameof(interactionResolver));
            }
            InteractionResolver = interactionResolver;
        }

        public bool IsInteractionAvailableForContext(string interactionName, IContext context)
        {
            return context.Scope.InteractionDefinitions.ContainsKey(interactionName);
        }

        public IInteraction GetInteractionByName(string interactionName, IEnumerable<object> paramValues, IContext context)
        {
            if (!IsInteractionAvailableForContext(interactionName, context))
            {
                throw new InteractionUnavailableException(interactionName);
            }
            InteractionDefinition interactionDefinition = context.Scope.InteractionDefinitions[interactionName];
            return InteractionResolver.Resolve(interactionDefinition, paramValues, context);
        }
    }
}
