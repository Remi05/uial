using System.Collections.Generic;
using Uial.Contexts;

namespace Uial.Interactions
{
    public interface IInteractionDefinition
    {
        string Name { get; }

        IInteraction Resolve(IContext context, IInteractionProvider interactionProvider, IEnumerable<string> paramValues);
    }
}
