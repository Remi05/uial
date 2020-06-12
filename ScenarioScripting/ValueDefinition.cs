using System;
using System.Collections.Generic;
using ScenarioScripting.Scopes;

namespace ScenarioScripting
{
    public class ValueDefinition
    {
        private object LitteralValue { get; set; }
        private string ReferenceName { get; set; }

        private ValueDefinition() { }

        public object Resolve(RuntimeScope scope)
        {
            if (LitteralValue != null)
            {
                return LitteralValue;
            }
            if (ReferenceName == null || !scope.ReferenceValues.ContainsKey(ReferenceName))
            {
                throw new ReferenceValueNotFoundException(ReferenceName);
            }
            return scope.ReferenceValues[ReferenceName];
        }

        public static ValueDefinition FromReference(string referenceName)
        {
            ValueDefinition valueDefinition = new ValueDefinition();
            valueDefinition.ReferenceName = referenceName;
            return valueDefinition;
        }

        public static ValueDefinition FromLitteral(object litteralValue)
        {
            ValueDefinition valueDefinition = new ValueDefinition();
            valueDefinition.LitteralValue = litteralValue;
            return valueDefinition;
        }
    }
}
