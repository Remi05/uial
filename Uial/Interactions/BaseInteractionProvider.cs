using System.Collections.Generic;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions;

public abstract class BaseInteractionProvider : IInteractionProvider
{
    protected delegate IInteraction InteractionFactory(IContext context, RuntimeScope scope, IEnumerable<string> paramValues);

    protected virtual IDictionary<string, InteractionFactory> KnownInteractions { get; }

    public bool IsKnownInteraction(string interactionName)
    {
        return KnownInteractions.ContainsKey(interactionName);
    }

    public IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues)
    {
        if (!IsKnownInteraction(interactionName))
        {
            throw new InteractionUnavailableException(interactionName);
        }
        return KnownInteractions[interactionName](context, scope, paramValues);
    }
}

