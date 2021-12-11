using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Interactions;

namespace Uial.Testing
{
    public class TestableResolver : ITestableResolver
    {
        private IBaseInteractionResolver BaseInteractionResolver { get; set; }

        public TestableResolver(IBaseInteractionResolver baseInteractionResolver)
        {
            BaseInteractionResolver = baseInteractionResolver;
        }

        public ITestable Resolve(TestableDefinition testableDefinition, IContext context)
        {
            if (testableDefinition is TestDefinition)
            {
                return Resolve((TestDefinition)testableDefinition, context);
            }
            if (testableDefinition is TestGroupDefinition)
            {
                return Resolve((TestGroupDefinition)testableDefinition, context);
            }
            return null;
        }

        public Test Resolve(TestDefinition testDefinition, IContext context)
        {
            IEnumerable<IInteraction> interactions = testDefinition.BaseInteractionDefinitions.Select(
                (interactionDefinition) => BaseInteractionResolver.Resolve(interactionDefinition, context)
            );
            return new Test(testDefinition.TestName, interactions);
        }


        public TestGroup Resolve(TestGroupDefinition testGroupDefinition, IContext context)
        {
            IEnumerable<ITestable> children = testGroupDefinition.ChildrenDefinitions.Select(
                (childDefinition) => Resolve(childDefinition, context)
            );
            return new TestGroup(testGroupDefinition.TestGroupName, children);
        }
    }
}
