using Uial.Definitions;
using Uial.Scopes;

namespace Uial.Contexts
{
    public interface IBaseContextResolver
    {
        IContext Resolve(BaseContextDefinition baseContextDefinition, IContext parentContext, RuntimeScope currentScope);
    }
}
