using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uial.Contexts
{
    public class ContextResolver : IContextResolver
    {
        public IContext ResolveContext(BaseContextDefinition baseContextDefinition)
        {

            if (!parentContext.Scope.ContextDefinitions.ContainsKey(baseContextDefinition.ContextName))
            {
                throw new ContextNotFoundException(baseContextDefinition.ContextName, parentContext);
            }
            IEnumerable<string> paramValues = ParamsValueDefinitions.Select((valueDefinition) => valueDefinition.Resolve(currentScope));
            IContext context = parentContext.Scope.ContextDefinitions[ContextName].Resolve(parentContext, paramValues);
            return Child?.Resolve(context, currentScope) ?? context;
        }
    }
}
