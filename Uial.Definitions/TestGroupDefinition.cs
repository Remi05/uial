using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Interactions;

namespace Uial.Definitions
{
    public class TestGroupDefinition
    {
        public string Name { get; protected set; }
        protected IEnumerable<TestableDefinition> ChildrenDefinitions { get; set; }

        public TestGroupDefinition(string name, IEnumerable<TestableDefinition> childrenDefinitions)
        {
            if (name == null || childrenDefinitions == null)
            {
                throw new ArgumentNullException(name == null ? nameof(name) : nameof(childrenDefinitions));
            }
            Name = name;
            ChildrenDefinitions = childrenDefinitions;
        }
    }
}
