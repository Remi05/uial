using System;
using System.Collections.Generic;

namespace Uial.DataModels
{
    public class ContextDefinition
    {
        public DefinitionScope Scope { get; protected set; }
        public string Name { get; protected set; }
        public IEnumerable<string> ParamNames { get; protected set; }
        public ContextScopeDefinition ContextScope { get; protected set; }

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
