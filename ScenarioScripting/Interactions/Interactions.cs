using System;
using System.Collections.Generic;
using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public class Interactions
    {
        public static IInteraction GetBasicInteractionByName(IContext context, string interactionName, IEnumerable<object> paramValues)
        {
            switch (interactionName)
            {
                case CloseWindow.Key:
                    return new CloseWindow(context);
                case Invoke.Key:
                    return new Invoke(context);
                case Scroll.Key:
                    return Scroll.FromRuntimeValues(context, paramValues);
                case Select.Key:
                    return new Select(context);
                case SetValue.Key:
                    return SetValue.FromRuntimeValues(context, paramValues);
                case Toggle.Key:
                    return new Toggle(context);
                default:
                    throw new InteractionUnavailableException(interactionName);
            }
        }
    }
}
