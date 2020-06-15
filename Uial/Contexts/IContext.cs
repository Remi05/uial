using System.Windows.Automation;
using Uial.Scopes;

namespace Uial.Contexts
{
    public interface IContext
    {
        RuntimeScope Scope { get; }

        string Name { get; }

        AutomationElement RootElement { get; }

        bool IsAvailable();
    }
}
