using System.Collections.Generic;

namespace Uial.Definitions
{
    public class BaseContextDefinition 
    {
        public string ContextName { get; private set; }
        public IEnumerable<ValueDefinition> ParamsValueDefinitions { get; private set; }
        public ConditionDefinition SpecifyingConditionDefinition { get; private set; }
        public BaseContextDefinition ChildContext { get; private set; }

        public BaseContextDefinition(string contextName, IEnumerable<ValueDefinition> paramsValueDefinitions, ConditionDefinition specifyingConditionDefinition = null, BaseContextDefinition childContext = null)
        {
            ContextName = contextName;
            ParamsValueDefinitions = paramsValueDefinitions;
            SpecifyingConditionDefinition = specifyingConditionDefinition;
            ChildContext = childContext;
        }
    }
}
