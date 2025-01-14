using Uial.DataModels;

namespace Uial.Contexts
{
    public interface IBaseContextResolver
    {
        IContext Resolve(BaseContextDefinition baseContextDefinition, IContext parentContext);
    }
}
