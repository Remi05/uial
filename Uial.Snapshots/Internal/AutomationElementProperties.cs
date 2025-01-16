using UIAutomationClient;

namespace Uial.Snapshots.Internal;

public class AutomationElementProperties : ElementProperties
{
    public IUIAutomationElement Element { get; set; }
}
