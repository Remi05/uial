using System;

namespace Uial.Definitions
{
    public class PropertyConditionDefinition
    {
        public string PropertyName { get; private set; }
        public ReferenceValueDefinition Value { get; private set; }

        public PropertyConditionDefinition(string propertyName, ReferenceValueDefinition value)
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
