using System;

namespace Uial.Definitions
{
    public class LiteralValueDefinition : ValueDefinition
    {
        public string LiteralValue { get; private set; }

        public LiteralValueDefinition(string literalValue) 
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
