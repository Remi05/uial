using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Definitions;
using Uial.Scopes;

namespace Uial.Contexts
{
    public class BaseContextResolver : IBaseContextResolver
    {
        private IContextResolver ContextResolver { get; set; }
        private IValueResolver ValueResolver { get; set; }

        public BaseContextResolver(IContextResolver contextResolver, IValueResolver valueResolver)
        {
            ContextResolver = contextResolver;
            ValueResolver = valueResolver;
        }

        public IContext Resolve(BaseContextDefinition baseContextDefintition, IContext parentContext, RuntimeScope currentScope)
        {
            if (baseContextDefintition == null)
            {
                return null;
            }

            if (!parentContext.Scope.ContextDefinitions.ContainsKey(baseContextDefintition.ContextName))
            {
                // TODO: Throw more specific exception.
                throw new Exception($"Context \"{baseContextDefintition.ContextName}\" doesn't exist in the current scope.");
            }

            ContextDefinition contextDefinition = parentContext.Scope.ContextDefinitions[baseContextDefintition.ContextName];

            if (baseContextDefintition.SpecifyingConditionDefinition != null)
            {
                var specifiedConditionDefinition = new CompositeConditionDefinition(new List<ConditionDefinition>() { contextDefinition.RootElementConditionDefiniton, baseContextDefintition.SpecifyingConditionDefinition  });
                contextDefinition = new ContextDefinition(contextDefinition.Scope, contextDefinition.ContextName, contextDefinition.ParamNames, specifiedConditionDefinition, contextDefinition.UniqueConditionDefinition);
            }

            IEnumerable<string> paramValues = baseContextDefintition.ParamsValueDefinitions.Select((valueDefinition) => ValueResolver.Resolve(valueDefinition, currentScope));
            IContext context = ContextResolver.Resolve(contextDefinition, parentContext, paramValues);
            return Resolve(baseContextDefintition.ChildContext, context, currentScope) ?? context;
        }
    }
}
