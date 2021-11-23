using System.Collections.Generic;

namespace Uial.Definitions
{
    public class DefinitionScope
    {
        public ISet<string> ReferenceValuesNames { get; private set; } = new HashSet<string>();
        public Dictionary<string, ContextDefinition> ContextDefinitions { get; private set; } = new Dictionary<string, ContextDefinition>();
        public Dictionary<string, InteractionDefinition> InteractionDefinitions { get; private set; } = new Dictionary<string, InteractionDefinition>();

        public DefinitionScope() { }

        public DefinitionScope(DefinitionScope scope)
        {
            ReferenceValuesNames = new HashSet<string>(scope.ReferenceValuesNames);
            ContextDefinitions = new Dictionary<string, ContextDefinition>(scope.ContextDefinitions);
            InteractionDefinitions = new Dictionary<string, InteractionDefinition>(scope.InteractionDefinitions);
        }
    }
}
