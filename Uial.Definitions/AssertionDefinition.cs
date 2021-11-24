using System.Collections.Generic;

namespace Uial.Definitions
{
    public class AssertionDefinition : BaseInteractionDefinition
    {
        public AssertionDefinition(string assertionName, IEnumerable<ValueDefinition> paramsValueDefinitions)
            : base(assertionName, paramsValueDefinitions) { }
    }
}
