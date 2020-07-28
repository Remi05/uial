using System.Collections.Generic;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions.Core
{
    public static class Interactions
    {
        public static IInteraction GetCoreInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues)
        {
            switch (interactionName)
            {
                case Close.Key:
                    return new Close(context as IWindowsVisualContext);
                case Collapse.Key:
                    return new Collapse(context as IWindowsVisualContext);
                case Expand.Key:
                    return new Expand(context as IWindowsVisualContext);
                case Focus.Key:
                    return new Focus(context as IWindowsVisualContext);
                case GetPropertyValue.Key:
                    return GetPropertyValue.FromRuntimeValues(context as IWindowsVisualContext, scope, paramValues);
                case GetRangeValue.Key:
                    return GetRangeValue.FromRuntimeValues(context as IWindowsVisualContext, scope, paramValues);
                case GetTextValue.Key:
                    return GetPropertyValue.FromRuntimeValues(context as IWindowsVisualContext, scope, paramValues);
                case Invoke.Key:
                    return new Invoke(context as IWindowsVisualContext);
                case IsAvailable.Key:
                    return IsAvailable.FromRuntimeValues(context, scope, paramValues);
                case Maximize.Key:
                    return new Maximize(context as IWindowsVisualContext);
                case Minimize.Key:
                    return new Minimize(context as IWindowsVisualContext);
                case Move.Key:
                    return Move.FromRuntimeValues(context as IWindowsVisualContext, paramValues);
                case Resize.Key:
                    return Resize.FromRuntimeValues(context as IWindowsVisualContext, paramValues);
                case Restore.Key:
                    return new Restore(context as IWindowsVisualContext);
                case Scroll.Key:
                    return Scroll.FromRuntimeValues(context as IWindowsVisualContext, paramValues);
                case Select.Key:
                    return new Select(context as IWindowsVisualContext);
                case SetRangeValue.Key:
                    return SetRangeValue.FromRuntimeValues(context as IWindowsVisualContext, paramValues);
                case SetTextValue.Key:
                    return SetTextValue.FromRuntimeValues(context as IWindowsVisualContext, paramValues);
                case Toggle.Key:
                    return new Toggle(context as IWindowsVisualContext);
                case Wait.Key:
                    return Wait.FromRuntimeValues(paramValues);
                case WaitUntilAvailable.Key:
                    return WaitUntilAvailable.FromRuntimeValues(context, paramValues);
                default:
                    throw new InteractionUnavailableException(interactionName);
            }
        }
    }
}
