using UIAutomationClient;
using Uial.Scopes;
using Uial.UnitTests.Contexts; // For MockContext, TODO: Review
using Uial.Windows.Contexts;

namespace Uial.UnitTests.Windows.Contexts
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
