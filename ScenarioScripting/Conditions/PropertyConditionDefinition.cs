using System;
using System.Windows.Automation;
using ScenarioScripting.Contexts;
using ScenarioScripting.Scopes;

namespace ScenarioScripting.Conditions
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
            object propertyValue = Controls.GetPropertyValue(Property, Value.Resolve(scope));
            return new PropertyCondition(Property, propertyValue);
        }
    }
}
