using UIAutomationClient;
using Uial.Scopes;

namespace Uial.Conditions
{
    public interface IConditionDefinition
    {
        IUIAutomationCondition Resolve(RuntimeScope scope);
    }
}
