using System;
using System.Collections.Generic;
using System.Linq;

namespace Uial.Definitions
{
    public class ConditionDefinition
    {
        public IEnumerable<PropertyConditionDefinition> PropertyConditions { get; private set; }

        public ConditionDefinition(IEnumerable<PropertyConditionDefinition> propertyConditions)
        {
            if (propertyConditions == null)
            {
                throw new ArgumentNullException(nameof(propertyConditions));
            }
            if (propertyConditions.Count() == 0)
            {
                throw new ArgumentException($"Parameter \"{nameof(propertyConditions)}\" must contain at least one ConditionDefinition.");
            }
            PropertyConditions = propertyConditions;
        }

        public override string ToString()
        {
            return $"{string.Join(", ", PropertyConditions)}";
        }
    }
}
