using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Automation;

namespace Uial.Conditions
{
    public static class Conditions
    {
        private static readonly IList<AutomationProperty> IdentifyingProperties = new List<AutomationProperty>()
        {
            AutomationElement.AutomationIdProperty,
            AutomationElement.ClassNameProperty,
            AutomationElement.ControlTypeProperty,
            AutomationElement.NameProperty,
        };

        public static IConditionDefinition GetConditionFromElement(AutomationElement element)
        {
            if (element == null)
            {
                return null;
            }

            var propertyConditions = new List<IConditionDefinition>();
            foreach (AutomationProperty property in IdentifyingProperties)
            {
                object propertyValue = element.GetCurrentPropertyValue(property);
                string propertyValueStr = PropertyValueToString(propertyValue);
                if (!string.IsNullOrWhiteSpace(propertyValueStr))
                {
                    var valueDefinition = ValueDefinition.FromLiteral(propertyValueStr);
                    var propertyCondition = new PropertyConditionDefinition(property, valueDefinition);
                    propertyConditions.Add(propertyCondition);
                }
            }
            return new CompositeConditionDefinition(propertyConditions);
        }

        private static string PropertyValueToString(object propertyValue)
        {
            if (propertyValue is ControlType)
            {
                return (propertyValue as ControlType)?.ToUialString();
            }
            return propertyValue?.ToString();
        }
    }
}
