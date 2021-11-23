using UIAutomationClient;

namespace Uial.Contexts.Windows
{
    public interface IWindowsVisualContext : IContext
    {
        IUIAutomationElement RootElement { get; }
    }
}
