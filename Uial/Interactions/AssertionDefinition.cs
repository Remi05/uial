using System.Collections.Generic;
using System.Linq;
using Uial.Assertions;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions
{
    public class AssertionDefinition : IBaseInteractionDefinition
    {
        public string InteractionName { get; protected set; }

        private IEnumerable<ValueDefinition> ParamsValueDefinitions { get; set; }

        public AssertionDefinition(string assertionName, IEnumerable<ValueDefinition> paramsValueDefinitions)
        {
            InteractionName = assertionName;
            ParamsValueDefinitions = paramsValueDefinitions;
        }

        public IInteraction Resolve(IContext parentContext, IInteractionProvider interactionProvider, RuntimeScope currentScope)
        {
            IEnumerable<string> paramValues = ParamsValueDefinitions.Select((valueDefinition) => valueDefinition.Resolve(currentScope));
            IAssertion assertion = Assertions.Assertions.GetAssertionByName(InteractionName, paramValues);
            return new AssertionInteraction(assertion);
        }
    }
}
