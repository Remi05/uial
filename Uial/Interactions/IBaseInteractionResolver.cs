using Uial.Contexts;
using Uial.Definitions;
using Uial.Scopes;

namespace Uial.Interactions
{
    public interface IBaseInteractionResolver
    {
        IInteraction Resolve(BaseInteractionDefinition baseInteractionDefinition, IContext parentContext, IInteractionProvider interactionProvider, RuntimeScope currentScope);
    }
}