
namespace ScenarioScripting.Assertions
{
    public class OrAssertion : IAssertion
    {
        private IAssertion First { get; set; }
        private IAssertion Second { get; set; }

        public OrAssertion(IAssertion first, IAssertion second)
        {
            First = first;
            Second = second;
        }

        public bool Assert()
        {
            return First.Assert() || Second.Assert();
        }
    }
}
