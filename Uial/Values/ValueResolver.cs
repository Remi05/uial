using Uial.DataModels;
using Uial.Values;

namespace Uial
{
    public class ValueResolver : IValueResolver
    {
        public object Resolve(ValueDefinition valueDefintion, IReferenceValueStore referenceValueStore)
        {
            if (valueDefintion is LiteralValueDefinition)
            {
                return (valueDefintion as LiteralValueDefinition)?.LiteralValue;
            }

            var referenceValueDefinition = valueDefintion as ReferenceValueDefinition;
            return referenceValueStore.GetValue(referenceValueDefinition.ReferenceName);
        }
    }
}
