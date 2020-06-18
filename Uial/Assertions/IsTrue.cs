using System.Collections.Generic;
using System.Linq;
using Uial.Interactions;

namespace Uial.Assertions
{
    public class IsTrue : IAssertion
    {
        public const string Key = "IsTrue";

        public string Name => Key;
        private bool BooleanValue { get; set; }

        public IsTrue(bool booleanValue)
        {
            BooleanValue = booleanValue;
        }

        public bool Assert()
        {
            return BooleanValue;
        }

        public static IsTrue FromRuntimeValues(IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            bool booleanValue = bool.Parse(paramValues.ElementAt(0));
            return new IsTrue(booleanValue);
        }
    }
}
