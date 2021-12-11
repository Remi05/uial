using System.Collections.Generic;

namespace Uial.DataModels
{
    public class ContextScopeDefinition
    {
        public string ContextType { get; private set; }
        public IEnumerable<ValueDefinition> Parameters { get; private set; }
        public ConditionDefinition ScopeCondition { get; private set; }

        public ContextScopeDefinition(string contextType, IEnumerable<ValueDefinition> parameters = null, ConditionDefinition scopeCondition = null)
        {
            ContextType = contextType;
            Parameters = parameters;
            ScopeCondition = scopeCondition;
        }
    }
}
