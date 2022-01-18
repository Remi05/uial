using System.Collections.Generic;
using Uial.Contexts;
using Uial.Interactions;

namespace Uial.Modules
{
    public class Module
    {
        public string Name { get; private set; }
        public ICollection<IInteractionProvider> InteractionProviders { get; private set; }
        public ICollection<IContextProvider> ContextProviders { get; private set; }

        public Module(string name, ICollection<IInteractionProvider> interactionProviders, ICollection<IContextProvider> contextProviders)
        {
            Name = name;
            InteractionProviders = interactionProviders;
            ContextProviders = contextProviders;
        }
    }
}
