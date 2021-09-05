using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Scopes;

namespace Uial.Contexts
{
    public class BaseContextDefinition
    {
        public string ContextName { get; private set; }
        private IEnumerable<ValueDefinition> ParamsValueDefinitions { get; set; }
        private BaseContextDefinition Child { get; set; }

        public BaseContextDefinition(string contextName, IEnumerable<ValueDefinition> paramsValueDefinitions, BaseContextDefinition child = null)
        {
            ContextName = contextName;
            ParamsValueDefinitions = paramsValueDefinitions;
            Child = child;
        }

        public IContext Resolve(IContext parentContext, RuntimeScope currentScope)
        {
            if (!parentContext.Scope.ContextDefinitions.ContainsKey(ContextName))
            {
                throw new ContextNotFoundException(ContextName, parentContext);
            }
            IEnumerable<string> paramValues = ParamsValueDefinitions.Select((valueDefinition) => valueDefinition.Resolve(currentScope));
            IContext context = parentContext.Scope.ContextDefinitions[ContextName].Resolve(parentContext, paramValues);
            return Child?.Resolve(context, currentScope) ?? context;
        }
    }
}
