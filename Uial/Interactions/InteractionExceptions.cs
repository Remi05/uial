using System;
using Uial.Assertions;

namespace Uial.Interactions
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

    public class ContextNotFoundInTimeException : Exception
    {
        private string ContextName { get; set; }
        private TimeSpan Timeout { get; set; }

        public override string Message => $"Could not find the context {ContextName} within the the given timeframe ({Timeout}).";

        public ContextNotFoundInTimeException(string contextName, TimeSpan timeout)
        {
            ContextName = contextName;
            Timeout = timeout;
        }
    }

    public class AssertionFailedException : Exception
    {
        private IAssertion Assertion { get; set; }

        public override string Message => $"Assertion failed: {Assertion}";

        public AssertionFailedException(IAssertion assertion)
        {
            Assertion = assertion;
        }
    }
}
