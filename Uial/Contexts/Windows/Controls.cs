using System.Collections.Generic;
using System.Text.RegularExpressions;
using UIAutomationClient;
using Uial.Conditions;

namespace Uial.Contexts.Windows
{
    public static class Controls
    {
        public static IConditionDefinition GetControlConditionDefinition(string controlTypeName, IConditionDefinition identifyingCondition)
        {
            ValueDefinition controlTypeRuntimeValue = ValueDefinition.FromLiteral(controlTypeName);
            IConditionDefinition controlTypeCondition = new PropertyConditionDefinition(UIA_PropertyIds.UIA_ControlTypePropertyId, controlTypeRuntimeValue);
            return new CompositeConditionDefinition(new List<IConditionDefinition>() { controlTypeCondition, identifyingCondition });
        }
    }
}
