using ScenarioScripting.Contexts;
using ScenarioScripting.Scopes;

namespace ScenarioScripting.Interactions
{
    public interface IBaseInteractionDefinition
    {
        IInteraction Resolve(IContext parentContext, RuntimeScope currentScope);
    }
}
