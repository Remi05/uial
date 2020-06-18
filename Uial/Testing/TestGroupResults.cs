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
                throw new ArgumentNullException(childrenResults == null ? "testName" : "childrenResults");
            }
            TestName = testName;
            ChildrenResults = childrenResults;
        }
    }
}
