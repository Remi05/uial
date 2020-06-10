using System;

namespace ScenarioScripting.Interactions
{
    public class InteractionUnavailableException : Exception
    {
        public override string Message => "This interaction is unavailable in the given context.";
    }
}
