using System;

namespace ScenarioScripting.Interactions
{
    public class InteractionUnavailableException : Exception
    {
        private string InteractionName { get; set; }

        public override string Message => $"Interaction \"{InteractionName}\" is unavailable in the given context.";

        public InteractionUnavailableException(string interactionName)
        {
            InteractionName = interactionName;
        }
    }

    public class InvalidParameterCountException : Exception
    {
        private int ExpectedParamsCount { get; set; }
        private int ReceivedParamsCount { get; set; }


        public override string Message => $"Exepected {ExpectedParamsCount} parameters but received {ReceivedParamsCount}.";

        public InvalidParameterCountException(int expectedParamsCount, int receivedParamsCount)
        {
            ExpectedParamsCount = expectedParamsCount;
            ReceivedParamsCount = receivedParamsCount;
        }
    }
}
