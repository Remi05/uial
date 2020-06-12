
namespace ScenarioScripting.Assertions
{
    public class Or : IAssertion
    {
        public string Name => "Or";

        private IAssertion First { get; set; }
        private IAssertion Second { get; set; }

        public Or(IAssertion first, IAssertion second)
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
