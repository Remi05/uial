﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using Uial.Scopes;

namespace Uial.Conditions
{
    public class CompositeConditionDefinition : IConditionDefinition
    {
        private IEnumerable<IConditionDefinition> ConditionDefinitions { get; set; }

        public CompositeConditionDefinition(IEnumerable<IConditionDefinition> conditionDefinitions)
        {
            if (conditionDefinitions == null)
            {
                throw new ArgumentNullException(nameof(conditionDefinitions));
            }
            if (conditionDefinitions.Count() == 0)
            {
                throw new ArgumentException($"Parameter \"{nameof(conditionDefinitions)}\" must contain at least one ConditionDefinition.");
            }
            ConditionDefinitions = conditionDefinitions;
        }

        public Condition Resolve(RuntimeScope scope)
        {
            Condition condition = null;
            foreach (IConditionDefinition conditionDefinition in ConditionDefinitions)
            {
                Condition childCondition = conditionDefinition.Resolve(scope);
                condition = condition == null ? childCondition : new AndCondition(condition, childCondition);
            }
            return condition;
        }

        public override string ToString()
        {
            return $"{string.Join(", ", ConditionDefinitions)}";
        }
    }
}
