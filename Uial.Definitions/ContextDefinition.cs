using System;
using System.Collections.Generic;

namespace Uial.Definitions
{
    public class ContextDefinition
    {
        public DefinitionScope Scope { get; private set; }
        public string ContextName { get; private set; }
        public IEnumerable<string> ParamNames { get; private set; }
        public ConditionDefinition RootElementConditionDefiniton { get; private set; }
        public ConditionDefinition UniqueConditionDefinition { get; private set; }

        public ContextDefinition(DefinitionScope scope, string contextName, IEnumerable<string> paramNames = null, ConditionDefinition rootElementConditionDefiniton = null, ConditionDefinition uniqueConditionDefinition = null)
        {
            if (contextName == null)
            {
                throw new ArgumentNullException(nameof(contextName));
            }    
            Scope = scope;
            ContextName = contextName;
            ParamNames = paramNames;
            RootElementConditionDefiniton = rootElementConditionDefiniton;
            UniqueConditionDefinition = uniqueConditionDefinition;
        }
    }
}
