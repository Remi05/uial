using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Definitions;
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

        public ITestable Resolve(TestableDefinition testableDefinition, IContext context, IInteractionProvider interactionProvider)
        {
            if (testableDefinition is TestDefinition)
            {
                return Resolve((TestDefinition)testableDefinition, context, interactionProvider);
            }
            if (testableDefinition is TestGroupDefinition)
            {
                return Resolve((TestGroupDefinition)testableDefinition, context, interactionProvider);
            }
            return null;
        }

        public Test Resolve(TestDefinition testDefinition, IContext context, IInteractionProvider interactionProvider)
        {
            IEnumerable<IInteraction> interactions = testDefinition.BaseInteractionDefinitions.Select(
                (interactionDefinition) => BaseInteractionResolver.Resolve(interactionDefinition, context, interactionProvider, context?.Scope)
            );
            return new Test(testDefinition.TestName, interactions);
        }


        public TestGroup Resolve(TestGroupDefinition testGroupDefinition, IContext context, IInteractionProvider interactionProvider)
        {
            IEnumerable<ITestable> children = testGroupDefinition.ChildrenDefinitions.Select(
                (childDefinition) => Resolve(childDefinition, context, interactionProvider)
            );
            return new TestGroup(testGroupDefinition.TestGroupName, children);
        }
    }
}
