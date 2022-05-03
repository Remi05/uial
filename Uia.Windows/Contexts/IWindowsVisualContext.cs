using UIAutomationClient;
using Uial.Contexts;

namespace Uial.Windows.Contexts
{
    public interface IWindowsVisualContext : IContext
    {
        IUIAutomationElement RootElement { get; }
    }
}
