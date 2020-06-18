
namespace Uial.Assertions
{
    public class Not : IAssertion
    {
        public const string Key = "Not";

        public string Name => Key;
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
