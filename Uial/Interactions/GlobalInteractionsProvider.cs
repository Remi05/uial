﻿using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Scopes;


namespace Uial.Interactions
{
    public class GlobalinteractionProvider : IInteractionProvider
    {
        protected IList<IInteractionProvider> InteractionProviders { get; set; }

        public GlobalinteractionProvider(IList<IInteractionProvider> interactionProviders)
        {
            if (interactionProviders == null)
            {
                throw new ArgumentNullException("interactionProviders");
            }
            InteractionProviders = interactionProviders;
        }

        public bool IsKnownInteraction(string interactionName)
        {
            return InteractionProviders.Any(interactionProvider => interactionProvider.IsKnownInteraction(interactionName));
        }

        public IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues)
        {
            IInteractionProvider interactionProvider = InteractionProviders.First(provider => provider.IsKnownInteraction(interactionName));
            return interactionProvider?.GetInteractionByName(context, scope, interactionName, paramValues);
        }
    }
}