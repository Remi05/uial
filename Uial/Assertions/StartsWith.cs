using System.Collections.Generic;
using System.Linq;
using Uial.Interactions;

namespace Uial.Assertions
{
    public class StartsWith : IAssertion
    {
        public const string Key = "StartsWith";

        public string Name => Key;
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

        public static StartsWith FromRuntimeValues(IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 2)
            {
                throw new InvalidParameterCountException(2, paramValues.Count());
            }
            string first = paramValues.ElementAt(0);
            string second = paramValues.ElementAt(1);
            return new StartsWith(first, second);
        }
    }
}
