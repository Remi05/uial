using ScenarioScripting.Scopes;

namespace ScenarioScripting.Contexts
{
    public interface IBaseContextDefinition
    {
        IContext Resolve(IContext parentContext, RuntimeScope currentScope);
    }
}
