using System.Collections.Generic;
using Uial.DataModels;

namespace Uial.Contexts
{
    public class ScopedContextProvider : IContextProvider
    {
        protected IContextProvider ContextProvider { get; set; }

        public ScopedContextProvider(IContextProvider contextProvider)
        {
            ContextProvider = contextProvider;
        }

        public bool IsKnownContext(ContextScopeDefinition contextScopeDefinition, IContext parentContext)
        {
            return parentContext.Scope.ContextDefinitions.ContainsKey(contextScopeDefinition.ContextType);
        }

        public IContext GetContextFromDefinition(ContextScopeDefinition contextScopeDefinition, IEnumerable<object> paramValues, IContext parentContext)
        {
            if (!IsKnownContext(contextScopeDefinition, parentContext))
            {
                throw new ContextNotFoundException(contextScopeDefinition.ContextType, parentContext);
            }
            ContextDefinition contextDefinition = parentContext.Scope.ContextDefinitions[contextScopeDefinition.ContextType];
            if (contextScopeDefinition.ScopeCondition != null)
            {
                var specifiedConditionDefinition = new CompositeConditionDefinition(new List<ConditionDefinition>() { contextDefinition.ContextScope.ScopeCondition, contextScopeDefinition.ScopeCondition });
                var specifiedContextScope = new ContextScopeDefinition(contextDefinition.ContextScope.ContextType, contextDefinition.ContextScope.Parameters, specifiedConditionDefinition);
                contextDefinition = new ContextDefinition(contextDefinition.Scope, contextDefinition.Name, contextDefinition.ParamNames, specifiedContextScope);
            }
            return ContextProvider.GetContextFromDefinition(contextDefinition.ContextScope, paramValues, parentContext);
        }
    }
}
