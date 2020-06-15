using System;
using System.Collections.Generic;
using ScenarioScripting.Scopes;

namespace ScenarioScripting
{
    public class ValueDefinition
    {
        private string LitteralValue { get; set; }
        private string ReferenceName { get; set; }

        private ValueDefinition() { }

        public string Resolve(RuntimeScope scope)
        {
            if (LitteralValue != null)
            {
                return LitteralValue;
            }
            if (scope == null || !scope.ReferenceValues.ContainsKey(ReferenceName))
            {
                throw new ReferenceValueNotFoundException(ReferenceName);
            }
            return scope.ReferenceValues[ReferenceName];
        }

        public static ValueDefinition FromReference(string referenceName)
        {
            if (referenceName == null)
            {
                throw new ArgumentNullException("referenceName");
            }
            ValueDefinition valueDefinition = new ValueDefinition();
            valueDefinition.ReferenceName = referenceName;
            return valueDefinition;
        }

        public static ValueDefinition FromLitteral(string litteralValue)
        {
            if (litteralValue == null)
            {
                throw new ArgumentNullException("litteralValue");
            }
            ValueDefinition valueDefinition = new ValueDefinition();
            valueDefinition.LitteralValue = litteralValue;
            return valueDefinition;
        }
    }
}
