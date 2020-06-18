using System.Collections.Generic;
using System.Linq;
using Uial.Interactions;

namespace Uial.Assertions
{
    public class AreEqual : IAssertion
    {
        public const string Key = "AreEqual";

        public string Name => Key;
        private object First { get; set; }
        private object Second { get; set; }

        public AreEqual(object first, object second)
        {
            First = first;
            Second = second;
        }

        public bool Assert()
        {
            return First == null && Second == null
                || First != null && First.Equals(Second);
        }

        public static AreEqual FromRuntimeValues(IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 2)
            {
                throw new InvalidParameterCountException(2, paramValues.Count());
            }
            string first = paramValues.ElementAt(0);
            string second = paramValues.ElementAt(1);
            return new AreEqual(first, second);
        }
    }
}
