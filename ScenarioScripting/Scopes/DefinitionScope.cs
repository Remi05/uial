using System.Collections.Generic;
using ScenarioScripting.Contexts;
using ScenarioScripting.Interactions;

namespace ScenarioScripting.Scopes
{
    public class DefinitionScope
    {
        public ISet<string> ReferenceValuesNames { get; private set; } = new HashSet<string>();
        public Dictionary<string, IContextDefinition> ContextDefinitions { get; private set; } = new Dictionary<string, IContextDefinition>();
        public Dictionary<string, IInteractionDefinition> InteractionDefinitions { get; private set; } = new Dictionary<string, IInteractionDefinition>();

        public DefinitionScope() { }

        public DefinitionScope(DefinitionScope scope)
        {
            ReferenceValuesNames = new HashSet<string>(scope.ReferenceValuesNames);
            ContextDefinitions = new Dictionary<string, IContextDefinition>(scope.ContextDefinitions);
            InteractionDefinitions = new Dictionary<string, IInteractionDefinition>(scope.InteractionDefinitions);
        }
    }
}
