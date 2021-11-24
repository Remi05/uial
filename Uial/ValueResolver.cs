using Uial.Definitions;
using Uial.Scopes;

namespace Uial
{
    public class ValueResolver : IValueResolver
    {
        public string Resolve(ValueDefinition valueDefintion, RuntimeScope scope)
        {
            if (valueDefintion is LiteralValueDefinition)
            {
                return (valueDefintion as LiteralValueDefinition)?.LiteralValue;
            }

            var referenceValueDefinition = valueDefintion as ReferenceValueDefinition;
            if (scope == null || !scope.ReferenceValues.ContainsKey(referenceValueDefinition.ReferenceName))
            {
                throw new ReferenceValueNotFoundException(referenceValueDefinition.ReferenceName);
            }
            return scope.ReferenceValues[referenceValueDefinition.ReferenceName];
        }
    }
}
