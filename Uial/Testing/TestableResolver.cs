using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Interactions;
using Uial.Values;

namespace Uial.Testing
{
    public class TestableResolver : ITestableResolver
    {
        private IBaseInteractionResolver BaseInteractionResolver { get; set; }

        public TestableResolver(IBaseInteractionResolver baseInteractionResolver)
        {
            BaseInteractionResolver = baseInteractionResolver;
        }

        public ITestable Resolve(TestableDefinition testableDefinition, IContext context, IReferenceValueStore referenceValueStore)
        {
            if (testableDefinition is TestDefinition)
            {
                return Resolve((TestDefinition)testableDefinition, context, referenceValueStore);
            }
            if (testableDefinition is TestGroupDefinition)
            {
                return Resolve((TestGroupDefinition)testableDefinition, context, referenceValueStore);
            }
            return null;
        }

        public Test Resolve(TestDefinition testDefinition, IContext context, IReferenceValueStore referenceValueStore)
        {
            var interactions = new List<IInteraction>();
            if (testDefinition.BaseInteractionDefinitions != null && testDefinition.BaseInteractionDefinitions.Count() > 0)
            {
                foreach (BaseInteractionDefinition baseInteractionDefinition in testDefinition.BaseInteractionDefinitions)
                {
                    IInteraction interaction = BaseInteractionResolver.Resolve(baseInteractionDefinition, context, referenceValueStore);
                    interactions.Add(interaction);
                }
            }

            return new Test(testDefinition.TestName, interactions);
        }


        public TestGroup Resolve(TestGroupDefinition testGroupDefinition, IContext context, IReferenceValueStore referenceValueStore)
        {
            var children = new List<ITestable>();
            if (testGroupDefinition.ChildrenDefinitions != null && testGroupDefinition.ChildrenDefinitions.Count() > 0)
            {
                foreach (TestableDefinition childDefinition in testGroupDefinition.ChildrenDefinitions)
                {
                    ITestable child = Resolve(childDefinition, context, referenceValueStore);
                    children.Add(child);
                }
            }

            return new TestGroup(testGroupDefinition.TestGroupName, children);
        }
    }
}
