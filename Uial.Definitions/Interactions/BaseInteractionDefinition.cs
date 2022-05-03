using System.Collections.Generic;

namespace Uial.DataModels
{
    public class BaseInteractionDefinition
    {
        public string InteractionName { get; protected set; }
        public IEnumerable<ValueDefinition> ParamsValueDefinitions { get; protected set; }
        public BaseContextDefinition ContextDefinition { get; protected set; }

        public BaseInteractionDefinition(string interactionName, IEnumerable<ValueDefinition> paramsValueDefinitions = null, BaseContextDefinition contextDefinition = null)
        {
            InteractionName = interactionName;
            ParamsValueDefinitions = paramsValueDefinitions;
            ContextDefinition = contextDefinition;
        }
    }
}
