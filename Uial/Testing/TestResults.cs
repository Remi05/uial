using System;

namespace Uial.Testing
{
    public class TestResults : ITestResults
    {
        public string TestName { get; protected set; }

        public bool Passed { get; protected set; }

        public TestResults(string testName, bool passed)
        {
            if (testName == null)
            {
                throw new ArgumentNullException("testName");
            }
            TestName = testName;
            Passed = passed;
        }

        public override string ToString()
        {
            string resultStr = Passed ? "Passed" : "Failed";
            return $"[{resultStr}] {TestName}";
        }
    }
}
