using System.Collections.Generic;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Scopes;

namespace Uial.Interactions
{
    public interface IInteractionResolver
    {
        IInteraction Resolve(InteractionDefinition interactionDefinition, IEnumerable<object> paramValues, IContext parentContext);
    }
}