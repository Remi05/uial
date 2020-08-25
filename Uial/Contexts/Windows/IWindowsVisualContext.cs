using System.Windows.Automation;
using Uial.Scopes;

namespace Uial.Contexts.Windows
{
    public interface IWindowsVisualContext : IContext
    {
        AutomationElement RootElement { get; }
    }
}
