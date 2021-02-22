using System.Collections.Generic;
using Uial.Contexts;
using Uial.Interactions;

namespace Uial.Scopes
{
    public class RuntimeScope
    {
        public Dictionary<string, string> ReferenceValues { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, IContextDefinition> ContextDefinitions { get; private set; }
        //protected Dictionary<string, IInteractionDefinition> InteractionDefinitions { get; private set; }


        public RuntimeScope(DefinitionScope definitionScope, Dictionary<string, string> referenceValues)
        {
            ReferenceValues = referenceValues;
            ContextDefinitions = definitionScope.ContextDefinitions;
            //InteractionDefinitions = definitionScope.InteractionDefinitions;
        }
    }
}
