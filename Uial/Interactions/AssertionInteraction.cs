using Uial.Assertions;

namespace Uial.Interactions
{
    public class AssertionInteraction : IInteraction
    {
        protected IAssertion Assertion { get; set; }
        public string Name => "Assert::" + Assertion.Name;

        public AssertionInteraction(IAssertion assertion)
        {
            Assertion = assertion;
        }

        public void Do()
        {
            if(!Assertion.Assert())
            {
                throw new AssertionFailedException(Assertion);
            }
        }
    }
}
