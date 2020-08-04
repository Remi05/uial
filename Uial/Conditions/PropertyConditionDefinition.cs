using System;
using System.Windows.Automation;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Conditions
{
    public class PropertyConditionDefinition : IConditionDefinition
    {
        private AutomationProperty Property { get; set; }
        private ValueDefinition Value { get; set; }

        public PropertyConditionDefinition(AutomationProperty property, ValueDefinition value)
        {
            if (property == null)
            {
                throw new ArgumentNullException("property");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            Property = property;
            Value = value;
        }

        public Condition Resolve(RuntimeScope scope)
        {
            object propertyValue = Properties.GetPropertyValue(Property, Value.Resolve(scope));
            return new PropertyCondition(Property, propertyValue);
        }

        public override string ToString()
        {
            return $"{Property.ToUialString()}={Value}";
        }
    }
}
