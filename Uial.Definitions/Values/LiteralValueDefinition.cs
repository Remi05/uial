using System;

namespace Uial.DataModels
{
    public class LiteralValueDefinition : ValueDefinition
    {
        public object LiteralValue { get; private set; }

        public LiteralValueDefinition(object literalValue) 
        {
            if (literalValue == null)
            {
                throw new ArgumentNullException(nameof(literalValue));
            }
            LiteralValue = literalValue;
        }

        public override string ToString()
        {
            return $"\"{LiteralValue}\"";
        }
    }
}
