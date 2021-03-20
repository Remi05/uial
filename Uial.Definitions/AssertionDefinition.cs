using System.Collections.Generic;
using System.Linq;
using Uial.Assertions;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Definitions
{
    public class AssertionDefinition
    {
        public string InteractionName { get; protected set; }

        private IEnumerable<ValueDefinition> ParamsValueDefinitions { get; set; }

        public AssertionDefinition(string assertionName, IEnumerable<ValueDefinition> paramsValueDefinitions)
        {
            InteractionName = assertionName;
            ParamsValueDefinitions = paramsValueDefinitions;
        }
    }
}
