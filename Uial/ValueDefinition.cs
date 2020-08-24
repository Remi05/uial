using System;
using System.Collections.Generic;
using Uial.Scopes;

namespace Uial
{
    public class ValueDefinition
    {
        private string LiteralValue { get; set; }
        private string ReferenceName { get; set; }

        private ValueDefinition() { }

        public string Resolve(RuntimeScope scope)
        {
            if (LiteralValue != null)
            {
                return LiteralValue;
            }
            if (scope == null || !scope.ReferenceValues.ContainsKey(ReferenceName))
            {
                throw new ReferenceValueNotFoundException(ReferenceName);
            }
            return scope.ReferenceValues[ReferenceName];
        }

        public override string ToString()
        {
            return ReferenceName?.ToString() ?? $"\"{LiteralValue}\"";
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

        public static ValueDefinition FromLiteral(string literalValue)
        {
            if (literalValue == null)
            {
                throw new ArgumentNullException("literalValue");
            }
            ValueDefinition valueDefinition = new ValueDefinition();
            valueDefinition.LiteralValue = literalValue;
            return valueDefinition;
        }
    }
}
