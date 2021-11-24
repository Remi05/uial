using Uial.Contexts;
using Uial.Definitions;
using Uial.Interactions;

namespace Uial.Testing
{
    public interface ITestableResolver
    {
        ITestable Resolve(TestableDefinition testDefinition, IContext context, IInteractionProvider interactionProvider);
        Test Resolve(TestDefinition testDefinition, IContext context, IInteractionProvider interactionProvider);
        TestGroup Resolve(TestGroupDefinition testDefinition, IContext context, IInteractionProvider interactionProvider);
    }
}