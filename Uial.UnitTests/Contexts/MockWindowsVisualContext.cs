using UIAutomationClient;
using Uial.Contexts.Windows;
using Uial.Scopes;

namespace Uial.UnitTests.Contexts.Windows
{
    class MockWindowsVisualContext : MockContext, IWindowsVisualContext
    {
        public IUIAutomationElement RootElement { get; protected set; }

        public MockWindowsVisualContext(RuntimeScope scope = null, string name = null, IUIAutomationElement rootElement = null, bool isAvailable = true)
            : base(scope, name, isAvailable)
        {
            RootElement = rootElement;
        }
    }
}
