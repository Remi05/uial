using System.Collections.Generic;

namespace Uial.DataModels
{
    public class DefinitionScope
    {
        public Dictionary<string, ContextDefinition> ContextDefinitions { get; protected set; } = new Dictionary<string, ContextDefinition>();
        public Dictionary<string, InteractionDefinition> InteractionDefinitions { get; protected set; } = new Dictionary<string, InteractionDefinition>();
        public Dictionary<string, StateDefinition> StateDefinitions { get; protected set; } = new Dictionary<string, StateDefinition>();

        public DefinitionScope() { }

        public DefinitionScope(DefinitionScope scope)
        {
            ContextDefinitions = new Dictionary<string, ContextDefinition>(scope.ContextDefinitions);
            InteractionDefinitions = new Dictionary<string, InteractionDefinition>(scope.InteractionDefinitions);
            StateDefinitions = new Dictionary<string, StateDefinition>(scope.StateDefinitions);
        }
    }
}
