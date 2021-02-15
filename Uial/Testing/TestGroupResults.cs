using System;
using System.Collections.Generic;
using System.Linq;

namespace Uial.Testing
{
    public class TestGroupResults : ITestResults
    {
        public string TestName { get; protected set; }
        protected IEnumerable<ITestResults> ChildrenResults { get; set; }

        public bool Passed => ChildrenResults.All((childResult) => childResult.Passed);

        public TestGroupResults(string testName, IEnumerable<ITestResults> childrenResults)
        {
            if (testName == null || childrenResults == null)
            {
                throw new ArgumentNullException(childrenResults == null ? nameof(testName) : nameof(childrenResults));
            }
            TestName = testName;
            ChildrenResults = childrenResults;
        }

        public override string ToString()
        {
            int passedCount = ChildrenResults.Count((childResult) => childResult.Passed);
            string resultStr = Passed ? "Passed" : "Failed";
            string header = $"[{resultStr}] {TestName} ({passedCount} of {ChildrenResults.Count()} passed)\n  ";
            string body = string.Join("\n", ChildrenResults).Replace("\n", "\n  ");
            return header + body;
        }
    }
}
