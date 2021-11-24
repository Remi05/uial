using Uial.Definitions;
using Uial.Scopes;

namespace Uial
{
    public interface IValueResolver
    {
        string Resolve(ValueDefinition valueDefintion, RuntimeScope scope);
    }
}
