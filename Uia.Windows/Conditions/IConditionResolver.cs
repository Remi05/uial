using UIAutomationClient;
using Uial.DataModels;
using Uial.Values;

namespace Uial.Windows.Conditions
{
    public interface IConditionResolver
    {
        IUIAutomationCondition Resolve(ConditionDefinition conditionDefinition, IReferenceValueStore referenceValueStore);
    }
}
