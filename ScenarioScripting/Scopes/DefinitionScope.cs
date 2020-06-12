using System.Collections.Generic;
using ScenarioScripting.Contexts;
using ScenarioScripting.Interactions;

namespace ScenarioScripting.Scopes
{
    public class DefinitionScope
    {
        public Dictionary<string, ValueDefinition> ReferenceValues { get; private set; } = new Dictionary<string, ValueDefinition>();
        public Dictionary<string, IContextDefinition> ContextDefinitions { get; private set; } = new Dictionary<string, IContextDefinition>();
        public Dictionary<string, IInteractionDefinition> InteractionDefinitions { get; private set; } = new Dictionary<string, IInteractionDefinition>();

        public DefinitionScope() { }

        public DefinitionScope(DefinitionScope scope)
        {
            ReferenceValues = new Dictionary<string, ValueDefinition>(scope.ReferenceValues);
            ContextDefinitions = new Dictionary<string, IContextDefinition>(scope.ContextDefinitions);
            InteractionDefinitions = new Dictionary<string, IInteractionDefinition>(scope.InteractionDefinitions);
        }
    }
}
