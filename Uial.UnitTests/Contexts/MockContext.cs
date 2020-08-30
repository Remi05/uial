using Uial.Contexts;
using Uial.Scopes;

namespace Uial.UnitTests.Contexts
{
    class MockContext : IContext
    {
        public string Name { get; protected set; }
        public RuntimeScope Scope { get; protected set; }
        protected bool Available { get; set;}

        public MockContext(string name = null, RuntimeScope scope = null, bool isAvailable = true)
        {
            Name = name;
            Scope = scope;
            Available = isAvailable;
        }

        public bool IsAvailable() => Available;
    }
}
