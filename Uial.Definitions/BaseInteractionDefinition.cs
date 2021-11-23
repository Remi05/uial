using System.Collections.Generic;

namespace Uial.Definitions
{
    public class BaseInteractionDefinition
    {
        public string InteractionName { get; private set; }
        public IEnumerable<ReferenceValueDefinition> ParamsValueDefinitions { get; private set; }
        public BaseContextDefinition ContextDefinition { get; private set; }

        public BaseInteractionDefinition(string interactionName, IEnumerable<ReferenceValueDefinition> paramsValueDefinitions, BaseContextDefinition contextDefinition = null)
        {
            InteractionName = interactionName;
            ParamsValueDefinitions = paramsValueDefinitions;
            ContextDefinition = contextDefinition;
        }
    }
}
