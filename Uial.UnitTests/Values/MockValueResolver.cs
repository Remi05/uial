using System.Collections.Generic;
using Uial.DataModels;
using Uial.Values;

namespace Uial.UnitTests.Values
{
    public class MockValueResolver : IValueResolver
    {
        public Dictionary<ValueDefinition, object> ValuesMap { get; protected set; }

        public MockValueResolver(Dictionary<ValueDefinition, object> valuesMap) 
        {
            ValuesMap = valuesMap;
        }

        public MockValueResolver()
            : this(new Dictionary<ValueDefinition, object>()) { }

        public object Resolve(ValueDefinition valueDefinition, IReferenceValueStore referenceValueStore)
        {
            var literalValueDefinition = valueDefinition as LiteralValueDefinition;
            if (literalValueDefinition != null)
            {
                return literalValueDefinition.LiteralValue;
            }
            return ValuesMap[valueDefinition];
        }
    }
}
