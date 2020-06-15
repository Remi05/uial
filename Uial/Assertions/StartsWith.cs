
namespace Uial.Assertions
{
    public class StartsWith : IAssertion
    {
        public string Name => "StartsWith";

        private string First { get; set; }
        private string Second { get; set; }

        public StartsWith(string first, string second)
        {
            First = first;
            Second = second;
        }

        public bool Assert()
        {
            return First != null && Second != null
                && First.StartsWith(Second);
        }
    }
}
