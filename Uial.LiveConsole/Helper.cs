using System.Collections.Generic;
using System.Windows.Automation;
using Uial.Conditions;
using Uial.Contexts;

namespace Uial.LiveConsole
{
    public static class Helper
    {
        static readonly List<AutomationProperty> IdentifyingProperties = new List<AutomationProperty>()
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

            List<IConditionDefinition> propertyConditions = new List<IConditionDefinition>();
            foreach (AutomationProperty property in IdentifyingProperties)
            {
                object propertyValue = element.GetCurrentPropertyValue(property);
                string propertyValueStr = PropertyValueToString(propertyValue);
                if (!string.IsNullOrWhiteSpace(propertyValueStr))
                {
                    ValueDefinition valueDefinition = ValueDefinition.FromLiteral(propertyValueStr);
                    PropertyConditionDefinition propertyCondition = new PropertyConditionDefinition(property, valueDefinition);
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
