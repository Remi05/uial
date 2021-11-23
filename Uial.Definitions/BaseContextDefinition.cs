using System.Collections.Generic;

namespace Uial.Definitions
{
    public class BaseContextDefinition
    {
        public string ContextName { get; private set; }
        public IEnumerable<ReferenceValueDefinition> ParamsValueDefinitions { get; private set; }
        public BaseContextDefinition ChildContext { get; private set; }

        public BaseContextDefinition(string contextName, IEnumerable<ReferenceValueDefinition> paramsValueDefinitions, BaseContextDefinition childContext = null)
        {
            ContextName = contextName;
            ParamsValueDefinitions = paramsValueDefinitions;
            ChildContext = childContext;
        }
    }
}
