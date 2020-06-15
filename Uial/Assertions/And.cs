
namespace Uial.Assertions
{
    public class And : IAssertion
    {
        public string Name => "And";

        private IAssertion First { get; set; }
        private IAssertion Second { get; set; }

        public And(IAssertion first, IAssertion second)
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
