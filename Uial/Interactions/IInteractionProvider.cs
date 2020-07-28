using System.Collections.Generic;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions
{
    public interface IInteractionProvider
    {
        bool IsKnownInteraction(string interactionName);

        IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues);
    }
}
