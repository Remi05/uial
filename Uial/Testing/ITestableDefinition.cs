using Uial.Contexts;
using Uial.Interactions;

namespace Uial.Testing
{
    public interface ITestableDefinition
    {
        string Name { get; }

        ITestable Resolve(IContext context, IInteractionsProvider interactionsProvider);
    }
}
