using Uial.Scopes;

namespace Uial.Contexts
{
    public interface IBaseContextDefinition
    {
        IContext Resolve(IContext parentContext, RuntimeScope currentScope);
    }
}
