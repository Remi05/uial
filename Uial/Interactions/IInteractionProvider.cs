using System.Collections.Generic;
using Uial.Contexts;

namespace Uial.Interactions
{
    public interface IInteractionProvider
    {
        //bool IsKnownInteraction(string interactionName);

        bool IsInteractionAvailableForContext(string interactionName, IContext context);

        IInteraction GetInteractionByName(string interactionName, IEnumerable<object> paramValues, IContext context);
    }
}
