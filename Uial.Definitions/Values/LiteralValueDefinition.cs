
namespace Uial.DataModels
{
    public class LiteralValueDefinition : ValueDefinition
    {
        public object LiteralValue { get; protected set; }

        public LiteralValueDefinition(object literalValue) 
        {
            LiteralValue = literalValue;
        }

        public override string ToString()
        {
            return $"\"{LiteralValue}\"";
        }
    }
}
