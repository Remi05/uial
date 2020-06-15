
namespace Uial.Assertions
{
    public class Contains : IAssertion
    {
        public string Name => "Contains";

        private string First { get; set; }
        private string Second { get; set; }

        public Contains(string first, string second)
        {
            First = first;
            Second = second;
        }

        public bool Assert()
        {
            return First != null && Second != null
                && First.Contains(Second);
        }
    }
}
