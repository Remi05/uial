
namespace ScenarioScripting.Assertions
{
    public class AndAssertion : IAssertion
    {
        private IAssertion First { get; set; }
        private IAssertion Second { get; set; }

        public AndAssertion(IAssertion first, IAssertion second)
        {
            First = first;
            Second = second;
        }

        public bool Assert()
        {
            return First.Assert() && Second.Assert();
        }
    }
}
