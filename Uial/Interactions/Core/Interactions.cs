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
                    return new Close(context);
                case Focus.Key:
                    return new Focus(context);
                case GetPropertyValue.Key:
                    return GetPropertyValue.FromRuntimeValues(context, scope, paramValues);
                case GetRangeValue.Key:
                    return GetRangeValue.FromRuntimeValues(context, scope, paramValues);
                case GetTextValue.Key:
                    return GetPropertyValue.FromRuntimeValues(context, scope, paramValues);
                case Invoke.Key:
                    return new Invoke(context);
                case IsAvailable.Key:
                    return IsAvailable.FromRuntimeValues(context, scope, paramValues);
                case Maximize.Key:
                    return new Maximize(context);
                case Minimize.Key:
                    return new Minimize(context);
                case Move.Key:
                    return Move.FromRuntimeValues(context, paramValues);
                case Resize.Key:
                    return Resize.FromRuntimeValues(context, paramValues);
                case Restore.Key:
                    return new Restore(context);
                case Scroll.Key:
                    return Scroll.FromRuntimeValues(context, paramValues);
                case Select.Key:
                    return new Select(context);
                case SetRangeValue.Key:
                    return SetRangeValue.FromRuntimeValues(context, paramValues);
                case SetTextValue.Key:
                    return SetTextValue.FromRuntimeValues(context, paramValues);
                case Toggle.Key:
                    return new Toggle(context);
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
