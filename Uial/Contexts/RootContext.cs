using System.Windows.Automation;
using Uial.Scopes;

namespace Uial.Contexts
{
    public class RootContext : IContext
    {
        public RuntimeScope Scope { get; private set; }
        public string Name => "Root";
        public AutomationElement RootElement => AutomationElement.RootElement; 

        public RootContext(RuntimeScope scope)
        {
            Scope = scope;
        }

        public bool IsAvailable()
        {
            return RootElement != null;
        }
    }
}
