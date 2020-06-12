using System.Collections.Generic;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions.Core
{
    public class Interactions
    {
        public static IInteraction GetCoreInteractionByName(IContext context, string interactionName, IEnumerable<object> paramValues)
        {
            switch (interactionName)
            {
                case Close.Key:
                    return new Close(context);
                case Invoke.Key:
                    return new Invoke(context);
                case Maximize.Key:
                    return new Maximize(context);
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
                default:
                    throw new InteractionUnavailableException(interactionName);
            }
        }
    }
}
