using System;
using UIAutomationClient;
using Uial.Contexts;
using Uial.Scopes;

using AutomationPropertyIdentifier = System.Int32;

namespace Uial.Conditions
{
    public class PropertyConditionDefinition : IConditionDefinition
    {
        private AutomationPropertyIdentifier Property { get; set; }
        private ValueDefinition Value { get; set; }

        public PropertyConditionDefinition(AutomationPropertyIdentifier property, ValueDefinition value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            Property = property;
            Value = value;
        }

        public IUIAutomationCondition Resolve(RuntimeScope scope)
        {
            object propertyValue = Properties.GetPropertyValue(Property, Value.Resolve(scope));
            return new CUIAutomation().CreatePropertyCondition(Property, propertyValue);
        }

        public override string ToString()
        {
            return $"{Property.GetPropertyUialString()}={Value}";
        }
    }
}
