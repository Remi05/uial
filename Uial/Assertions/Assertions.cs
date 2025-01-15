using System.Collections.Generic;

namespace Uial.Assertions
{
    public static class Assertions
    {
        public static IAssertion GetAssertionByName(string assertionName, IEnumerable<string> paramValues)
        {
            switch (assertionName)
            {
                case AreEqual.Key:
                    return AreEqual.FromRuntimeValues(paramValues);
                case Contains.Key:
                    return Contains.FromRuntimeValues(paramValues);
                case IsFalse.Key:
                    return IsFalse.FromRuntimeValues(paramValues);
                case IsTrue.Key:
                    return IsTrue.FromRuntimeValues(paramValues);
                case StartsWith.Key:
                    return StartsWith.FromRuntimeValues(paramValues);
                default:
                    throw new UnknownAssertionException(assertionName);
            }
        }
    }
}
