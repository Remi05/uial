using System.Collections.Generic;
using Uial.Contexts;
using Uial.Interactions;

namespace Uial.UnitTests.Interactions
{
    class MockInteractionDefinition : IInteractionDefinition
    {
        public string Name { get; protected set; }
        protected IInteraction Interaction { get; set; }

        public MockInteractionDefinition(string name, IInteraction interaction)
        {
            Name = name;
            Interaction = interaction;
        }

        public IInteraction Resolve(IContext context, IInteractionProvider interactionProvider, IEnumerable<string> parmaValues)
        {
            return Interaction;
        }
    }
}
