using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Conditions;
using Uial.Interactions;
using Uial.Scopes;

namespace Uial.Definitions
{
    public class ContextDefinition 
    {
        public DefinitionScope Scope { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<string> ParamNames { get; private set; }
        private ConditionDefinition RootElementConditionDefiniton { get; set; }
        private ConditionDefinition UniqueConditionDefinition { get; set; }

        public ContextDefinition(DefinitionScope scope, string name, IEnumerable<string> paramNames, ConditionDefinition rootElementConditionDefiniton = null, ConditionDefinition uniqueConditionDefinition = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }    
            Scope = scope;
            Name = name;
            ParamNames = paramNames;
            RootElementConditionDefiniton = rootElementConditionDefiniton;
            UniqueConditionDefinition = uniqueConditionDefinition;
        }
    }
}
