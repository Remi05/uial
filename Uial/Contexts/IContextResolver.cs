using System.Collections.Generic;
using Uial.Definitions;

namespace Uial.Contexts
{
    public interface IContextResolver
    {
        IContext Resolve(ContextDefinition contextDefinition, IContext parentContext, IEnumerable<string> paramValues);
    }
}
