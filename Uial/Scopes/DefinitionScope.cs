using System.Collections.Generic;
using Uial.Contexts;
using Uial.Interactions;

namespace Uial.Scopes
{
    public class DefinitionScope
    {
        public ISet<string> ReferenceValuesNames { get; private set; } = new HashSet<string>();
        public Dictionary<string, ContextDefinition> ContextDefinitions { get; private set; } = new Dictionary<string, ContextDefinition>();
        public Dictionary<string, IInteractionDefinition> InteractionDefinitions { get; private set; } = new Dictionary<string, IInteractionDefinition>();

        public DefinitionScope() { }

        public DefinitionScope(DefinitionScope scope)
        {
            ReferenceValuesNames = new HashSet<string>(scope.ReferenceValuesNames);
            ContextDefinitions = new Dictionary<string, ContextDefinition>(scope.ContextDefinitions);
            InteractionDefinitions = new Dictionary<string, IInteractionDefinition>(scope.InteractionDefinitions);
        }
    }
}
