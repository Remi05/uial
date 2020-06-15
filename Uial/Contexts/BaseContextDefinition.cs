using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Scopes;

namespace Uial.Contexts
{
    public class BaseContextDefinition : IBaseContextDefinition
    {
        public string ContextName { get; private set; }
        private IEnumerable<ValueDefinition> ParamsValueDefinitions { get; set; }
        private IBaseContextDefinition Child { get; set; }

        public BaseContextDefinition(string contextName, IEnumerable<ValueDefinition> paramsValueDefinitions, IBaseContextDefinition child = null)
        {
            ContextName = contextName;
            ParamsValueDefinitions = paramsValueDefinitions;
            Child = child;
        }

        public IContext Resolve(IContext parentContext, RuntimeScope currentScope)
        {
            if (!parentContext.Scope.ContextDefinitions.ContainsKey(ContextName))
            {
                // TODO: Throw more specific exception.
                throw new Exception($"Context \"{ContextName}\" doesn't exist in the current scope.");
            }
            IEnumerable<string> paramValues = ParamsValueDefinitions.Select((valueDefinition) => valueDefinition.Resolve(currentScope));
            IContext context = parentContext.Scope.ContextDefinitions[ContextName].Resolve(parentContext, paramValues);
            return Child?.Resolve(context, currentScope) ?? context;
        }
    }
}
