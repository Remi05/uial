using System;
using System.Collections.Generic;

namespace Uial.Definitions
{
    public class TestDefinition : TestableDefinition
    {
        public string TestName { get; protected set; }
        public IEnumerable<BaseInteractionDefinition> BaseInteractionDefinitions { get; private set; }

        public TestDefinition(string testName, IEnumerable<BaseInteractionDefinition> baseInteractionDefinitions)
        {
            if (testName == null || baseInteractionDefinitions == null)
            {
                throw new ArgumentNullException(testName == null ? nameof(testName) : nameof(baseInteractionDefinitions));
            }
            TestName = testName;
            BaseInteractionDefinitions = baseInteractionDefinitions;
        }
    }
}
