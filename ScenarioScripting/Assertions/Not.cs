
namespace ScenarioScripting.Assertions
{
    public class NotAssertion : IAssertion
    {
        private IAssertion Assertion { get; set; } 

        public NotAssertion(IAssertion assertion)
        {
            Assertion = assertion;
        }

        public bool Assert()
        {
            return !Assertion.Assert();
        }
    }
}
