using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Definitions;
using Uial.Scopes;

namespace Uial.Interactions
{
    public class BaseInteractionResolver : IBaseInteractionResolver
    {
        private IBaseContextResolver BaseContextResolver { get; set; }
        private IValueResolver ValueResolver { get; set; }

        public BaseInteractionResolver(IBaseContextResolver baseContextResolver, IValueResolver valueResolver)
        {
            BaseContextResolver = baseContextResolver;
            ValueResolver = valueResolver;
        }

        public IInteraction Resolve(BaseInteractionDefinition baseInteractionDefinition, IContext parentContext, IInteractionProvider interactionProvider, RuntimeScope currentScope)
        {
            IContext context = BaseContextResolver.Resolve(baseInteractionDefinition.ContextDefinition, parentContext, currentScope) ?? parentContext;
            IEnumerable<string> paramValues = baseInteractionDefinition.ParamsValueDefinitions.Select((valueDefinition) => ValueResolver.Resolve(valueDefinition, currentScope));

            //if (context.Scope.InteractionDefinitions.ContainsKey(baseInteractionDefinition.InteractionName))
            //{
            //    return context.Scope.InteractionDefinitions[baseInteractionDefinition.InteractionName].Resolve(context, interactionProvider, paramValues);
            //}

            return interactionProvider.GetInteractionByName(context, currentScope, baseInteractionDefinition.InteractionName, paramValues);
        }
    }
}
