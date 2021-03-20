using System.Collections.Generic;
using System.Linq;
using Uial.Scopes;

namespace Uial.Definitions
{
    public class BaseInteractionDefinition
    {
        private string InteractionName { get; set; }
        private IEnumerable<ValueDefinition> ParamsValueDefinitions { get; set; }
        private BaseContextDefinition ContextDefinition { get; set; }

        public BaseInteractionDefinition(string interactionName, IEnumerable<ValueDefinition> paramsValueDefinitions, BaseContextDefinition contextDefinition = null)
        {
            InteractionName = interactionName;
            ParamsValueDefinitions = paramsValueDefinitions;
            ContextDefinition = contextDefinition;
        }
    }
}
