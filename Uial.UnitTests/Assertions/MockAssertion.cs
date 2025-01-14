using System;
using Uial.Assertions;

namespace Uial.UnitTests.Assertions
{
    public class MockAssertion : IAssertion
    {
        public string Name { get; protected set; }
        protected Func<bool> AssertFunc { get; set; }
        protected int RunCount { get; set; } = 0;

        public bool WasRun => RunCount > 0;
        public bool WasRunOnce => RunCount == 1;

        public MockAssertion(string name, Func<bool> assertFunc)
        {
            Name = name;
            AssertFunc = assertFunc;
        }

        public bool Assert()
        {
            bool assertionResult = AssertFunc();
            ++RunCount;
            return assertionResult;
        }
    }
}
