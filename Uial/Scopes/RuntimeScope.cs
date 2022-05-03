using System.Collections.Generic;
using Uial.DataModels;
using Uial.Values;

namespace Uial.Scopes
{
    public class RuntimeScope
    {
        public Dictionary<string, ContextDefinition> ContextDefinitions { get; private set; } = new Dictionary<string, ContextDefinition>();
        public Dictionary<string, InteractionDefinition> InteractionDefinitions { get; private set; } = new Dictionary<string, InteractionDefinition>();
        public IReferenceValueStore ReferenceValueStore { get; private set; }

        public RuntimeScope(DefinitionScope definitionScope, IReferenceValueStore referenceValueStore)
        {
            ContextDefinitions = definitionScope.ContextDefinitions;
            InteractionDefinitions = definitionScope.InteractionDefinitions;
            ReferenceValueStore = referenceValueStore;
        }
    }
}
