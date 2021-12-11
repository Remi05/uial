using System.Collections.Generic;
using Uial.DataModels;

namespace Uial.UnitTests.Values
{
    class MockValueResolver : IValueResolver
    {
        public Dictionary<ValueDefinition, object> ValuesMap { get; protected set; } = new Dictionary<ValueDefinition, object>();

        public object Resolve(ValueDefinition valueDefintion)
        {
            return ValuesMap[valueDefintion];
        }
    }
}
