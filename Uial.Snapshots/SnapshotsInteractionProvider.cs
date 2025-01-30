using System.Collections.Generic;
using Uial.Contexts;
using Uial.Contexts.Windows;
using Uial.Interactions;
using Uial.Scopes;

namespace Uial.Snapshots;

public class SnapshotsInteractionProvider : IInteractionProvider
{
    protected delegate IInteraction VisualInteractionFactory(IWindowsVisualContext windowsVisualContext, RuntimeScope scope, IEnumerable<string> paramValues);

    protected IDictionary<string, VisualInteractionFactory> KnownInteractions = new Dictionary<string, VisualInteractionFactory>()
    {
        { ScreenshotInteraction.Key, (windowsVisualContext, _, __) => new ScreenshotInteraction(windowsVisualContext) },
        { SnapshotInteraction.Key,   (windowsVisualContext, _, __) => new SnapshotInteraction(windowsVisualContext) },
    };

    public bool IsKnownInteraction(string interactionName)
    {
        return KnownInteractions.ContainsKey(interactionName);
    }

    public IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues)
    {
        IWindowsVisualContext windowsVisualContext = context as IWindowsVisualContext;
        if (windowsVisualContext == null)
        {
            throw new InvalidWindowsVisualContextException(context);
        }
        else if (!IsKnownInteraction(interactionName))
        {
            throw new InteractionUnavailableException(interactionName);
        }
        return KnownInteractions[interactionName](windowsVisualContext, scope, paramValues);
    }
}
