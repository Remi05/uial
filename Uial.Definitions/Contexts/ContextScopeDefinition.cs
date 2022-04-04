using System.Collections.Generic;

namespace Uial.DataModels
{
    public class ContextScopeDefinition
    {
        public string ContextType { get; protected set; }
        public IEnumerable<ValueDefinition> Parameters { get; protected set; }
        public ConditionDefinition ScopeCondition { get; protected set; }

        public ContextScopeDefinition(string contextType, IEnumerable<ValueDefinition> parameters = null, ConditionDefinition scopeCondition = null)
        {
            ContextType = contextType;
            Parameters = parameters;
            ScopeCondition = scopeCondition;
        }
    }
}
