using System.Collections.Generic;
using Uial.Contexts;
using Uial.Contexts.Windows;
using Uial.Interactions;
using Uial.Scopes;

namespace Uial.Snapshots;

public class SnapshotsInteractionProvider : IInteractionProvider
{
    public bool IsKnownInteraction(string interactionName)
    {
        return interactionName == SnapshotInteraction.Key;
    }

    public IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues)
    {
        IWindowsVisualContext windowsVisualContext = context as IWindowsVisualContext;
        if (windowsVisualContext == null)
        {
            throw new InvalidWindowsVisualContextException(context);
        }
        if (!IsKnownInteraction(interactionName))
        {
            throw new InteractionUnavailableException(interactionName);
        }
        return new SnapshotInteraction(windowsVisualContext);
    }
}
