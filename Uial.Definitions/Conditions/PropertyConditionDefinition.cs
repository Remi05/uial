using System;

namespace Uial.DataModels
{
    public class PropertyConditionDefinition : ConditionDefinition
    {
        public string PropertyName { get; protected set; }
        public ValueDefinition Value { get; protected set; }

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
