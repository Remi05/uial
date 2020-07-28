using System.Collections.Generic;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions.Windows
{
    public class VisualInteractionProvider : IInteractionProvider
    {
        protected ISet<string> KnownInteractions = new HashSet<string>()
        {
            Close.Key,
            Collapse.Key,
            Expand.Key,
            Focus.Key,
            GetPropertyValue.Key,
            GetRangeValue.Key,
            GetTextValue.Key,
            Invoke.Key,
            Maximize.Key,
            Minimize.Key,
            Move.Key,
            Resize.Key,
            Restore.Key,
            Scroll.Key,
            Select.Key,
            SetRangeValue.Key,
            SetTextValue.Key,
            Toggle.Key,
        };

        public bool IsKnownInteraction(string interactionName)
        {
            return KnownInteractions.Contains(interactionName);
        }

        public IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues)
        {
            IWindowsVisualContext windowsVisualContext = context as IWindowsVisualContext;
            if (windowsVisualContext == null)
            {
                // throw
            }    
            
            switch (interactionName)
            {
                case Close.Key:
                    return new Close(windowsVisualContext);
                case Collapse.Key:
                    return new Collapse(windowsVisualContext);
                case Expand.Key:
                    return new Expand(windowsVisualContext);
                case Focus.Key:
                    return new Focus(windowsVisualContext);
                case GetPropertyValue.Key:
                    return GetPropertyValue.FromRuntimeValues(windowsVisualContext, scope, paramValues);
                case GetRangeValue.Key:
                    return GetRangeValue.FromRuntimeValues(windowsVisualContext, scope, paramValues);
                case GetTextValue.Key:
                    return GetPropertyValue.FromRuntimeValues(windowsVisualContext, scope, paramValues);
                case Invoke.Key:
                    return new Invoke(windowsVisualContext);
                case Maximize.Key:
                    return new Maximize(windowsVisualContext);
                case Minimize.Key:
                    return new Minimize(windowsVisualContext);
                case Move.Key:
                    return Move.FromRuntimeValues(windowsVisualContext, paramValues);
                case Resize.Key:
                    return Resize.FromRuntimeValues(windowsVisualContext, paramValues);
                case Restore.Key:
                    return new Restore(windowsVisualContext);
                case Scroll.Key:
                    return Scroll.FromRuntimeValues(windowsVisualContext, paramValues);
                case Select.Key:
                    return new Select(windowsVisualContext);
                case SetRangeValue.Key:
                    return SetRangeValue.FromRuntimeValues(windowsVisualContext, paramValues);
                case SetTextValue.Key:
                    return SetTextValue.FromRuntimeValues(windowsVisualContext, paramValues);
                case Toggle.Key:
                    return new Toggle(windowsVisualContext);
                default:
                    throw new InteractionUnavailableException(interactionName);
            }
        }
    }
}
