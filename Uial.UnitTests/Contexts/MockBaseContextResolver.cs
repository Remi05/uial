using System.Collections.Generic;
using Uial.Contexts;
using Uial.DataModels;

namespace Uial.UnitTests.Contexts
{
    public class MockBaseContextResolver : IBaseContextResolver
    {
        public Dictionary<BaseContextDefinition, IContext> ContextsMap { get; protected set; } = new Dictionary<BaseContextDefinition, IContext>();

        public IContext Resolve(BaseContextDefinition baseContextDefinition, IContext parentContext)
        {
            return ContextsMap[baseContextDefinition];
        }
    }
}
