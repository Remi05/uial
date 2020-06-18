using System.Collections.Generic;
using System.Linq;
using Uial.Interactions;

namespace Uial.Assertions
{
    public class IsFalse : IAssertion
    {
        public const string Key = "IsFalse";

        public string Name => Key;
        private bool BooleanValue { get; set; }

        public IsFalse(bool booleanValue)
        {
            BooleanValue = booleanValue;
        }

        public bool Assert()
        {
            return !BooleanValue;
        }

        public static IsFalse FromRuntimeValues(IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            bool booleanValue = bool.Parse(paramValues.ElementAt(0));
            return new IsFalse(booleanValue);
        }
    }
}
