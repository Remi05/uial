using System.Collections.Generic;
using UIAutomationClient;
using Uial.DataModels;

using AutomationIdentifier = System.Int32;
using AutomationPropertyIdentifier = System.Int32;
using AutomationControlTypeIdentifier = System.Int32;

namespace Uial.Windows.Conditions
{
    public static class Conditions
    {
        private static readonly IList<AutomationPropertyIdentifier> IdentifyingProperties = new List<AutomationPropertyIdentifier>()
        {
            UIA_PropertyIds.UIA_AutomationIdPropertyId,
            UIA_PropertyIds.UIA_ClassNamePropertyId,
            UIA_PropertyIds.UIA_ControlTypePropertyId,
            UIA_PropertyIds.UIA_NamePropertyId,
        };

        public static ConditionDefinition GetConditionFromElement(IUIAutomationElement element)
        {
            if (element == null)
            {
                return null;
            }

            var propertyConditions = new List<ConditionDefinition>();
            foreach (AutomationPropertyIdentifier property in IdentifyingProperties)
            {
                object propertyValue = element.GetCurrentPropertyValue(property);
                string propertyValueStr = PropertyValueToString(propertyValue);
                if (!string.IsNullOrWhiteSpace(propertyValueStr))
                {
                    string propertyName = Properties.GetPropertyUialString(property);
                    var valueDefinition = new LiteralValueDefinition(propertyValueStr);
                    var propertyCondition = new PropertyConditionDefinition(propertyName, valueDefinition);
                    propertyConditions.Add(propertyCondition);
                }
            }
            return new CompositeConditionDefinition(propertyConditions);
        }

        private static string PropertyValueToString(object propertyValue)
        {
            if (propertyValue is AutomationIdentifier && Properties.IsControlTypeIdentifier((AutomationIdentifier)propertyValue))
            {
                return ((AutomationControlTypeIdentifier)propertyValue).GetControlTypeUialString();
            }
            return propertyValue?.ToString();
        }
    }
}
