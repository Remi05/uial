using System.Windows.Automation;
using Uial.Scopes;

namespace Uial.Contexts
{
    public abstract class AbstractContext : IContext
    {
        public string Name { get; protected set; }
        public RuntimeScope Scope { get; protected set; }
        public Condition UniqueCondition { get; protected set; }
        public abstract AutomationElement RootElement { get; }

        public bool IsAvailable()
        {
            return RootElement != null
                && (UniqueCondition == null
                || RootElement.FindFirst(TreeScope.Subtree, UniqueCondition) != null);
        }
    }
}
