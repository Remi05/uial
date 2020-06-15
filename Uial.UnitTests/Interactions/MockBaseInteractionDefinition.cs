using Uial.Contexts;
using Uial.Interactions;
using Uial.Scopes;

namespace Uial.UnitTests.Interactions
{
    class MockBaseInteractionDefinition : IBaseInteractionDefinition
    {
        protected IInteraction Interaction { get; set; }

        public MockBaseInteractionDefinition(IInteraction interaction)
        {
            Interaction = interaction;
        }

        public IInteraction Resolve(IContext context, RuntimeScope scope)
        {
            return Interaction;
        }
    }
}
