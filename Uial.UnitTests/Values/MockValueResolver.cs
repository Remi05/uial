using System.Collections.Generic;
using Uial.DataModels;
using Uial.Values;

namespace Uial.UnitTests.Values
{
    class MockValueResolver : IValueResolver
    {
        public Dictionary<ValueDefinition, object> ValuesMap { get; protected set; } = new Dictionary<ValueDefinition, object>();

        public object Resolve(ValueDefinition valueDefintion, IReferenceValueStore referenceValueStore)
        {
            return ValuesMap[valueDefintion];
        }
    }
}
