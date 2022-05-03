using System;

namespace Uial.DataModels
{
    public class PropertyConditionDefinition : ConditionDefinition
    {
        public string PropertyName { get; protected set; }
        public ValueDefinition Value { get; protected set; }

        public PropertyConditionDefinition(string propertyName, ValueDefinition value)
        {
            if (propertyName == null || value == null)
            {
                throw new ArgumentNullException(propertyName == null ? nameof(propertyName) : nameof(value));
            }
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException($"{nameof(propertyName)} cannot be empty or white space.");
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
