using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Interactions;

namespace Uial.Scopes
{
    public class RuntimeScope
    {
        public Dictionary<string, string> ReferenceValues { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, IContextDefinition> ContextDefinitions { get; private set; } = new Dictionary<string, IContextDefinition>();
        public Dictionary<string, IInteractionDefinition> InteractionDefinitions { get; private set; } = new Dictionary<string, IInteractionDefinition>();

        public RuntimeScope(DefinitionScope definitionScope, Dictionary<string, string> referenceValues)
        {
            ReferenceValues = referenceValues;
            ContextDefinitions = new Dictionary<string, IContextDefinition>(definitionScope.ContextDefinitions);
            InteractionDefinitions = new Dictionary<string, IInteractionDefinition>(definitionScope.InteractionDefinitions);
        }
    }
}
