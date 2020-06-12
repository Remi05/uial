using System.Windows.Automation;
using ScenarioScripting.Scopes;

namespace ScenarioScripting.Conditions
{
    public interface IConditionDefinition
    {
        Condition Resolve(RuntimeScope scope);
    }
}
