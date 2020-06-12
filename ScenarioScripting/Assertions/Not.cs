
namespace ScenarioScripting.Assertions
{
    public class Not : IAssertion
    {
        public string Name => "Not";

        private IAssertion Assertion { get; set; } 

        public Not(IAssertion assertion)
        {
            Assertion = assertion;
        }

        public bool Assert()
        {
            return !Assertion.Assert();
        }
    }
}
