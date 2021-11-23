using System;
using System.Collections.Generic;

namespace Uial.Definitions
{
    public class TestDefinition : TestableDefinition
    {
        public string TestName { get; protected set; }
        protected IEnumerable<BaseInteractionDefinition> BaseInteractionDefinitions { get; set; }

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
