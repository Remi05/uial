using System;
using System.Collections.Generic;
using System.Linq;

namespace Uial.DataModels
{
    public class CompositeConditionDefinition : ConditionDefinition
    {
        public IEnumerable<ConditionDefinition> Conditions { get; protected set; }

        public CompositeConditionDefinition(IEnumerable<ConditionDefinition> conditions)
        {
            if (conditions == null)
            {
                throw new ArgumentNullException(nameof(conditions));
            }
            if (conditions.Count() == 0)
            {
                throw new ArgumentException($"Parameter \"{nameof(conditions)}\" must contain at least one ConditionDefinition.");
            }
            Conditions = conditions;
        }

        public override string ToString()
        {
            return $"{string.Join(", ", Conditions)}";
        }
    }
}
