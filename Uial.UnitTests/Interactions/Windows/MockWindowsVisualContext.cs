using System.Windows.Automation;
using Uial.Contexts.Windows;
using Uial.Scopes;
using Uial.UnitTests.Contexts;

namespace Uial.UnitTests.Interactions.Windows
{
    class MockWindowsVisualContext : MockContext, IWindowsVisualContext
    {
        public AutomationElement RootElement { get; protected set; }

        public MockWindowsVisualContext(string name = null, RuntimeScope scope = null, AutomationElement rootElement = null, bool isAvailable = true)
            : base(name, scope, isAvailable)
        {
            RootElement = rootElement;
        }
    }
}
