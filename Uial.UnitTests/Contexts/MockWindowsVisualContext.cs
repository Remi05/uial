using System.Windows.Automation;
using Uial.Contexts.Windows;
using Uial.Scopes;

namespace Uial.UnitTests.Contexts.Windows
{
    class MockWindowsVisualContext : MockContext, IWindowsVisualContext
    {
        public AutomationElement RootElement { get; protected set; }

        public MockWindowsVisualContext(RuntimeScope scope = null, string name = null, AutomationElement rootElement = null, bool isAvailable = true)
            : base(scope, name, isAvailable)
        {
            RootElement = rootElement;
        }
    }
}
