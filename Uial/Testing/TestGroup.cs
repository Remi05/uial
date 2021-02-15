using System;
using System.Collections.Generic;
using System.Linq;

namespace Uial.Testing
{
    public class TestGroup : ITestable
    {
        public string Name { get; protected set; }

        protected IEnumerable<ITestable> Children { get; set; }

        public TestGroup(string name, IEnumerable<ITestable> children)
        {
            if (name == null || children == null)
            {
                throw new ArgumentNullException(name == null ? nameof(name) : nameof(children));
            }
            Name = name;
            Children = children;
        }

        public ITestResults RunTest()
        {
            IEnumerable<ITestResults> childrenResults = Children.Select((child) => child.RunTest());
            return new TestGroupResults(Name, childrenResults);
        }
    }
}
