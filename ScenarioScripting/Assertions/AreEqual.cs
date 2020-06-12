
namespace ScenarioScripting.Assertions
{
    public class AreEqual : IAssertion
    {
        public string Name => "AreEqual";

        private object First { get; set; }
        private object Second { get; set; }

        public AreEqual(object first, object second)
        {
            First = first;
            Second = second;
        }

        public bool Assert()
        {
            return (First == null && Second == null)
                || First.Equals(Second);
        }
    }
}
