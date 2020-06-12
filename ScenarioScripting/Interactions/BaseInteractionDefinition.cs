using System.Collections.Generic;
using System.Linq;
using ScenarioScripting.Contexts;
using ScenarioScripting.Scopes;

namespace ScenarioScripting.Interactions
{
    public class BaseInteractionDefinition
    {
        private string InteractionName { get; set; }
        private IEnumerable<ValueDefinition> ParamsValueDefinitions { get; set; }
        private IBaseContextDefinition ContextDefinition { get; set; }

        public BaseInteractionDefinition(string interactionName, IEnumerable<ValueDefinition> paramsValueDefinitions, IBaseContextDefinition contextDefinition = null)
        {
            InteractionName = interactionName;
            ParamsValueDefinitions = paramsValueDefinitions;
            ContextDefinition = contextDefinition;
        }

        public IInteraction Resolve(IContext parentContext, RuntimeScope currentScope)
        {
            IContext context = ContextDefinition?.Resolve(parentContext, currentScope) ?? parentContext;
            IEnumerable<object> paramValues = ParamsValueDefinitions.Select((valueDefinition) => valueDefinition.Resolve(currentScope));

            if (context.Scope.InteractionDefinitions.ContainsKey(InteractionName))
            {
                return context.Scope.InteractionDefinitions[InteractionName].Resolve(context, paramValues);
            }

            return Interactions.GetBasicInteractionByName(context, InteractionName, paramValues);
        }
    }
}
