using System;
using System.Collections.Generic;

namespace Uial.Definitions
{
    public class InteractionDefinition
    {
        public DefinitionScope Scope { get; private set; }
        public string InteractionName { get; private set; }
        public IEnumerable<string> ParamNames { get; private set; }
        public IEnumerable<BaseInteractionDefinition> BaseInteractionDefinitions { get; private set; }

        public InteractionDefinition(DefinitionScope scope, string interactionName, IEnumerable<string> paramNames, IEnumerable<BaseInteractionDefinition> baseInteractionDefinitions)
        {
            if (interactionName == null)
            {
                throw new ArgumentNullException(nameof(interactionName));
            }
            Scope = scope;
            InteractionName = interactionName;
            ParamNames = paramNames;
            BaseInteractionDefinitions = baseInteractionDefinitions;
        }
    }
}
