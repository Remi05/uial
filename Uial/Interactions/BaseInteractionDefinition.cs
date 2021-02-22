using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions
{
    public class BaseInteractionDefinition : IBaseInteractionDefinition
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
            IEnumerable<string> paramValues = ParamsValueDefinitions.Select((valueDefinition) => valueDefinition.Resolve(currentScope));
            return context.InteractionProvider.GetInteractionByName(context, currentScope, InteractionName, paramValues);
        }
    }
}
