using System.Windows.Automation;
using ScenarioScripting.Scopes;

namespace ScenarioScripting.Contexts
{
    public interface IContext
    {
        RuntimeScope Scope { get; }

        string Name { get; }

        AutomationElement RootElement { get; }
    }
}
