using System;
using System.Collections.Generic;

namespace Uial.DataModels
{
    public class ContextDefinition
    {
        public DefinitionScope Scope { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<string> ParamNames { get; private set; }
        public ContextScopeDefinition ContextScope { get; private set; }

        public ContextDefinition(DefinitionScope scope, string name, IEnumerable<string> paramNames = null, ContextScopeDefinition contextScope = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }    
            Scope = scope;
            Name = name;
            ParamNames = paramNames;
            ContextScope = contextScope;
        }
    }
}
