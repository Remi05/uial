using System;

namespace Uial.Definitions
{
    public class PropertyConditionDefinition : ConditionDefinition
    {
        public string PropertyName { get; private set; }
        public ValueDefinition Value { get; private set; }

        public PropertyConditionDefinition(string propertyName, ValueDefinition value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            PropertyName = propertyName;
            Value = value;
        }

        public override string ToString()
        {
            return $"{PropertyName}={Value}";
        }
    }
}
