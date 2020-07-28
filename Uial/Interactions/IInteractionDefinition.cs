using System.Collections.Generic;
using Uial.Contexts;

namespace Uial.Interactions
{
    public interface IInteractionDefinition
    {
        string Name { get; }

        IInteraction Resolve(IContext context, IInteractionsProvider interactionsProvider, IEnumerable<string> paramValues);
    }
}
