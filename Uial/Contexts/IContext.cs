using Uial.Interactions;
using Uial.Scopes;

namespace Uial.Contexts
{
    public interface IContext
    {
        string Name { get; }
        RuntimeScope Scope { get; }
        IInteractionProvider InteractionProvider { get; }

        bool IsAvailable();
    }
}
