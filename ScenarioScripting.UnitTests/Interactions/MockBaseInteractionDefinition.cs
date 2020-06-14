using ScenarioScripting.Contexts;
using ScenarioScripting.Interactions;
using ScenarioScripting.Scopes;

namespace ScenarioScripting.UnitTests.Interactions
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
