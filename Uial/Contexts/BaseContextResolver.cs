using System.Collections.Generic;
using System.Linq;
using Uial.DataModels;

namespace Uial.Contexts
{
    public class BaseContextResolver : IBaseContextResolver
    {
        private IContextProvider ContextProvider { get; set; }
        private IValueResolver ValueResolver { get; set; }

        public BaseContextResolver(IContextProvider contextProvider, IValueResolver valueResolver)
        {
            ContextProvider = contextProvider;
            ValueResolver = valueResolver;
        }

        public IContext Resolve(BaseContextDefinition baseContextDefintition, IContext parentContext)
        {
            if (baseContextDefintition == null)
            {
                return null;
            }            
            IEnumerable<object> paramValues = baseContextDefintition.ContextScope.Parameters.Select((valueDefinition) => ValueResolver.Resolve(valueDefinition, parentContext.Scope.ReferenceValueStore));
            IContext context = ContextProvider.GetContextFromDefinition(baseContextDefintition.ContextScope, paramValues, parentContext);
            return Resolve(baseContextDefintition.ChildContext, context) ?? context;
        }
    }
}
