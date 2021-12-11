using System.Collections.Generic;

namespace Uial.DataModels
{
    public class BaseInteractionDefinition
    {
        public string InteractionName { get; private set; }
        public IEnumerable<ValueDefinition> ParamsValueDefinitions { get; private set; }
        public BaseContextDefinition ContextDefinition { get; private set; }

        public BaseInteractionDefinition(string interactionName, IEnumerable<ValueDefinition> paramsValueDefinitions = null, BaseContextDefinition contextDefinition = null)
        {
            InteractionName = interactionName;
            ParamsValueDefinitions = paramsValueDefinitions;
            ContextDefinition = contextDefinition;
        }
    }
}
