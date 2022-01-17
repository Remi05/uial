using System.Collections.Generic;
using Uial.Contexts;
using Uial.Interactions;
using Uial.Scopes;
using Uial.Values;
using Uial.Windows.Contexts;

namespace Uial.Windows.Interactions
{
    public class VisualInteractionProvider : IInteractionProvider
    {
        protected delegate IInteraction VisualInteractionFactory(IWindowsVisualContext windowsVisualContext, IEnumerable<object> paramValues, IReferenceValueStore referenceValueStore);

        protected IDictionary<string, VisualInteractionFactory> KnownInteractions = new Dictionary<string, VisualInteractionFactory>()
        {
            { Close.Key,            (windowsVisualContext, _, __) => new Close(windowsVisualContext) },
            { Collapse.Key,         (windowsVisualContext, _, __) => new Collapse(windowsVisualContext) },
            { Expand.Key,           (windowsVisualContext, _, __) => new Expand(windowsVisualContext) },
            { Focus.Key,            (windowsVisualContext, _, __) => new Focus(windowsVisualContext) },
            { GetPropertyValue.Key, (windowsVisualContext, paramValues, referenceValueStore) => GetPropertyValue.FromRuntimeValues(windowsVisualContext, paramValues, referenceValueStore) },
            { GetRangeValue.Key,    (windowsVisualContext, paramValues, referenceValueStore) => GetRangeValue.FromRuntimeValues(windowsVisualContext, paramValues, referenceValueStore) },
            { GetTextValue.Key,     (windowsVisualContext, paramValues, referenceValueStore) => GetPropertyValue.FromRuntimeValues(windowsVisualContext, paramValues, referenceValueStore) },
            { Invoke.Key,           (windowsVisualContext, _, __) => new Invoke(windowsVisualContext) },
            { Maximize.Key,         (windowsVisualContext, _, __) => new Maximize(windowsVisualContext) },
            { Minimize.Key,         (windowsVisualContext, _, __) => new Minimize(windowsVisualContext) },
            { Move.Key,             (windowsVisualContext, paramValues, _) => Move.FromRuntimeValues(windowsVisualContext, paramValues) },
            { Resize.Key,           (windowsVisualContext, paramValues, _) => Resize.FromRuntimeValues(windowsVisualContext, paramValues) },
            { Restore.Key,          (windowsVisualContext, _, __) => new Restore(windowsVisualContext) },
            { Scroll.Key,           (windowsVisualContext, paramValues, _) =>  Scroll.FromRuntimeValues(windowsVisualContext, paramValues) },
            { Select.Key,           (windowsVisualContext, _, __) => new Select(windowsVisualContext) },
            { SetRangeValue.Key,    (windowsVisualContext, paramValues, _) => SetRangeValue.FromRuntimeValues(windowsVisualContext, paramValues) },
            { SetTextValue.Key,     (windowsVisualContext, paramValues, _) => SetTextValue.FromRuntimeValues(windowsVisualContext, paramValues) },
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
            return KnownInteractions[interactionName](context as IWindowsVisualContext, paramValues, null); // TODO: Add value store
        }
    }
}
