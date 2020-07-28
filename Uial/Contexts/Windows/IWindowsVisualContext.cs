using System.Windows.Automation;
using Uial.Scopes;

namespace Uial.Contexts
{
    public interface IWindowsVisualContext : IContext
    {
        AutomationElement RootElement { get; }
    }
}
