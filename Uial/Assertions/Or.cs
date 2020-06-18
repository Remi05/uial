
namespace Uial.Assertions
{
    public class Or : IAssertion
    {
        public const string Key = "Or";

        public string Name => Key;
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
