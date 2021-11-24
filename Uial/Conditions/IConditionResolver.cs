using UIAutomationClient;
using Uial.Definitions;
using Uial.Scopes;

namespace Uial.Conditions
{
    public interface IConditionResolver
    {
        IUIAutomationCondition Resolve(ConditionDefinition conditionDefinition, RuntimeScope scope);
    }
}
