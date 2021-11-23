using System;
using System.Collections.Generic;

namespace Uial.Definitions
{
    public class TestGroupDefinition : TestableDefinition
    {
        public string TestGroupName { get; protected set; }
        public IEnumerable<TestableDefinition> ChildrenDefinitions { get; protected set; }

        public TestGroupDefinition(string testGroupName, IEnumerable<TestableDefinition> childrenDefinitions)
        {
            if (testGroupName == null || childrenDefinitions == null)
            {
                throw new ArgumentNullException(testGroupName == null ? nameof(testGroupName) : nameof(childrenDefinitions));
            }
            TestGroupName = testGroupName;
            ChildrenDefinitions = childrenDefinitions;
        }
    }
}
