using ScenarioScripting.Contexts;
using ScenarioScripting.Scopes;

namespace ScenarioScripting.Interactions
{
    interface IBaseInteractionDefinition
    {
        IInteraction Resolve(IContext parentContext, RuntimeScope currentScope);
    }
}
