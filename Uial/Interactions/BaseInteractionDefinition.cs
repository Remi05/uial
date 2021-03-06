﻿using System.Collections.Generic;
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

        public IInteraction Resolve(IContext parentContext, IInteractionProvider interactionProvider, RuntimeScope currentScope)
        {
            IContext context = ContextDefinition?.Resolve(parentContext, currentScope) ?? parentContext;
            IEnumerable<string> paramValues = ParamsValueDefinitions.Select((valueDefinition) => valueDefinition.Resolve(currentScope));

            if (context.Scope.InteractionDefinitions.ContainsKey(InteractionName))
            {
                return context.Scope.InteractionDefinitions[InteractionName].Resolve(context, interactionProvider, paramValues);
            }

            return interactionProvider.GetInteractionByName(context, currentScope, InteractionName, paramValues);
        }
    }
}
