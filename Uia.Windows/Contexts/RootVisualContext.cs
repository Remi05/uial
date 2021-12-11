using UIAutomationClient;
using Uial.Scopes;

namespace Uial.Windows.Contexts
{
    public class RootVisualContext : IWindowsVisualContext
    {
        private IUIAutomation UIAutomation { get; set; } = new CUIAutomation();
        public RuntimeScope Scope { get; private set; }
        public string Name => "RootVisualContext";
        public IUIAutomationElement RootElement => UIAutomation.GetRootElement(); 

        public RootVisualContext(RuntimeScope scope)
        {
            Scope = scope;
        }

        public bool IsAvailable()
        {
            return RootElement != null;
        }
    }
}
