using System.Collections.Generic;
using Uial.Interactions;

namespace Uial.Modules
{
    public class Module
    {
        public string Name { get; private set; }
        public IEnumerable<IInteractionProvider> InteractionProviders { get; private set; }

        public Module(string name, IEnumerable<IInteractionProvider> interactionProviders)
        {
            Name = name;
            InteractionProviders = interactionProviders;
        }
    }
}
