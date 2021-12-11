using System.Collections.Generic;
using Uial.DataModels;

namespace Uial.Contexts
{
    public interface IContextResolver
    {
        IContext Resolve(ContextDefinition contextDefinition, IEnumerable<object> paramValues, IContext parentContext);
    }
}
