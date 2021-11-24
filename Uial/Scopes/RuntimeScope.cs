using System.Collections.Generic;
using Uial.Definitions;

namespace Uial.Scopes
{
    public class RuntimeScope
    {
        public Dictionary<string, string> ReferenceValues { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, ContextDefinition> ContextDefinitions { get; private set; } = new Dictionary<string, ContextDefinition>();
        public Dictionary<string, InteractionDefinition> InteractionDefinitions { get; private set; } = new Dictionary<string, InteractionDefinition>();

        public RuntimeScope(DefinitionScope definitionScope, Dictionary<string, string> referenceValues)
        {
            ReferenceValues = referenceValues;
            ContextDefinitions = definitionScope.ContextDefinitions;
            InteractionDefinitions = definitionScope.InteractionDefinitions;
        }
    }
}
