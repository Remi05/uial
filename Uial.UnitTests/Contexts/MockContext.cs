using Uial.Contexts;
using Uial.Scopes;

namespace Uial.UnitTests.Contexts
{
    public class MockContext : IContext
    {
        public RuntimeScope Scope { get; protected set; }
        public string Name { get; protected set; }
        protected bool Available { get; set;}

        public MockContext(RuntimeScope scope = null, string name = null, bool isAvailable = true)
        {
            Scope = scope;
            Name = name;
            Available = isAvailable;
        }

        public bool IsAvailable() => Available;
    }
}
