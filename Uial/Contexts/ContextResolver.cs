using System.Collections.Generic;
using System.Linq;
using Uial.DataModels;
using Uial.Interactions;
using Uial.Scopes;

namespace Uial.Contexts
{
    public class ContextResolver : IContextResolver
    {
        public IContext Resolve(ContextDefinition contextDefinition, IEnumerable<object> paramValues, IContext parentContext)
        {
            if (paramValues.Count() != contextDefinition.ParamNames.Count())
            {
                throw new InvalidParameterCountException(contextDefinition.ParamNames.Count(), paramValues.Count());
            }

            //parentContext.Scope.ReferenceValues
            var referenceValues = new Dictionary<string, object>();
            for (int i = 0; i < contextDefinition.ParamNames.Count(); ++i)
            {
                referenceValues[contextDefinition.ParamNames.ElementAt(i)] = paramValues.ElementAt(i);
            }
            // TODO: Fix
            return null;
        }
    }
}
