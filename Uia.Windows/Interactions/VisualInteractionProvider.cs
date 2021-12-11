using System.Collections.Generic;
using Uial.Contexts;
using Uial.Interactions;
using Uial.Windows.Contexts;
using Uial.Scopes;

namespace Uial.Windows.Interactions
{
    public class VisualInteractionProvider : IInteractionProvider
    {
        protected delegate IInteraction VisualInteractionFactory(IWindowsVisualContext windowsVisualContext, RuntimeScope scope, IEnumerable<object> paramValues);

        protected IDictionary<string, VisualInteractionFactory> KnownInteractions = new Dictionary<string, VisualInteractionFactory>()
        {
            { Close.Key,            (windowsVisualContext, _, __) => new Close(windowsVisualContext) },
            { Collapse.Key,         (windowsVisualContext, _, __) => new Collapse(windowsVisualContext) },
            { Expand.Key,           (windowsVisualContext, _, __) => new Expand(windowsVisualContext) },
            { Focus.Key,            (windowsVisualContext, _, __) => new Focus(windowsVisualContext) },
            { GetPropertyValue.Key, (windowsVisualContext, scope, paramValues) => GetPropertyValue.FromRuntimeValues(windowsVisualContext, scope, paramValues) },
            { GetRangeValue.Key,    (windowsVisualContext, scope, paramValues) => GetRangeValue.FromRuntimeValues(windowsVisualContext, scope, paramValues) },
            { GetTextValue.Key,     (windowsVisualContext, scope, paramValues) => GetPropertyValue.FromRuntimeValues(windowsVisualContext, scope, paramValues) },
            { Invoke.Key,           (windowsVisualContext, _, __) => new Invoke(windowsVisualContext) },
            { Maximize.Key,         (windowsVisualContext, _, __) => new Maximize(windowsVisualContext) },
            { Minimize.Key,         (windowsVisualContext, _, __) => new Minimize(windowsVisualContext) },
            { Move.Key,             (windowsVisualContext, _, paramValues) => Move.FromRuntimeValues(windowsVisualContext, paramValues) },
            { Resize.Key,           (windowsVisualContext, _, paramValues) => Resize.FromRuntimeValues(windowsVisualContext, paramValues) },
            { Restore.Key,          (windowsVisualContext, _, __) => new Restore(windowsVisualContext) },
            { Scroll.Key,           (windowsVisualContext, _, paramValues) =>  Scroll.FromRuntimeValues(windowsVisualContext, paramValues) },
            { Select.Key,           (windowsVisualContext, _, __) => new Select(windowsVisualContext) },
            { SetRangeValue.Key,    (windowsVisualContext, _, paramValues) => SetRangeValue.FromRuntimeValues(windowsVisualContext, paramValues) },
            { SetTextValue.Key,     (windowsVisualContext, _, paramValues) => SetTextValue.FromRuntimeValues(windowsVisualContext, paramValues) },
            { Toggle.Key,           (windowsVisualContext, _, __) => new Toggle(windowsVisualContext) },
        };

        public bool IsInteractionAvailableForContext(string interactionName, IContext context)
        {
            return (context is IWindowsVisualContext) && KnownInteractions.ContainsKey(interactionName);
        }

        public IInteraction GetInteractionByName(string interactionName, IEnumerable<object> paramValues, IContext context)
        {
            if (!IsInteractionAvailableForContext(interactionName, context))
            {
                throw new InteractionUnavailableException(interactionName);
            }
            return KnownInteractions[interactionName](context as IWindowsVisualContext, context.Scope, paramValues); // TODO: Fix runtime scope
        }
    }
}
