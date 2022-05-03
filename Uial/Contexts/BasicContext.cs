using Uial.Scopes;

namespace Uial.Contexts
{
    public class BasicContext : IContext
    {
        public string Name { get; protected set; }
        public RuntimeScope Scope { get; protected set; }

        public BasicContext(string name, RuntimeScope scope)
        {
            Name = name;
            Scope = scope;
        }

        public bool IsAvailable() => true;
    }
}
