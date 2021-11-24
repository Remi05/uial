using System.Collections.Generic;
using Uial.Contexts;
using Uial.Definitions;

namespace Uial.Interactions
{
    public interface IInteractionResolver
    {
        IInteraction Resolve(InteractionDefinition interactionDefinition, IContext parentContext, IInteractionProvider interactionProvider, IEnumerable<string> paramValues);
    }
}