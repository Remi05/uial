using System.Collections.Generic;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions
{
    public interface IInteractionsProvider
    {
        bool IsKnownInteraction(string interactionName);

        IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues);
    }
}
