using System;

namespace Uial.Definitions
{
    public class PropertyConditionDefinition
    {
        private string PropertyName { get; set; }
        private ValueDefinition Value { get; set; }

        public PropertyConditionDefinition(string propertyName, ValueDefinition value)
        {
            if (propertyName == null || value == null)
            {
                throw new ArgumentNullException(propertyName == null ? nameof(propertyName) : nameof(value));
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
