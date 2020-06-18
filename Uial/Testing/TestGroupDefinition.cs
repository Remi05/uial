using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;

namespace Uial.Testing
{
    public class TestGroupDefinition : ITestableDefinition
    {
        public string Name { get; protected set; }
        protected IEnumerable<ITestableDefinition> ChildrenDefinitions { get; set; }

        public TestGroupDefinition(string name, IEnumerable<ITestableDefinition> childrenDefinitions)
        {
            if (name == null || childrenDefinitions == null)
            {
                throw new ArgumentNullException(name == null ? "name" : "childrenDefinitions");
            }
            Name = name;
            ChildrenDefinitions = childrenDefinitions;
        }

        public ITestable Resolve(IContext context)
        {
            IEnumerable<ITestable> children = ChildrenDefinitions.Select(
                (childDefinition) => childDefinition.Resolve(context)
            );
            return new TestGroup(Name, children);
        }
    }
}
