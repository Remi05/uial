using System.Collections.Generic;

namespace Uial.DataModels
{
    public class DefinitionScope
    {
        public Dictionary<string, ContextDefinition> ContextDefinitions { get; private set; } = new Dictionary<string, ContextDefinition>();
        public Dictionary<string, InteractionDefinition> InteractionDefinitions { get; private set; } = new Dictionary<string, InteractionDefinition>();
        public Dictionary<string, StateDefinition> StateDefinitions { get; private set; } = new Dictionary<string, StateDefinition>();

        public DefinitionScope() { }

        public DefinitionScope(DefinitionScope scope)
        {
            ContextDefinitions = new Dictionary<string, ContextDefinition>(scope.ContextDefinitions);
            InteractionDefinitions = new Dictionary<string, InteractionDefinition>(scope.InteractionDefinitions);
            StateDefinitions = new Dictionary<string, StateDefinition>(scope.StateDefinitions);
        }
    }
}
