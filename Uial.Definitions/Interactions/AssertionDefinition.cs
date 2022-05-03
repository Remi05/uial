using System.Collections.Generic;

namespace Uial.DataModels
{
    public class AssertionDefinition : BaseInteractionDefinition
    {
        public AssertionDefinition(string assertionName, IEnumerable<ValueDefinition> paramsValueDefinitions)
            : base(assertionName, paramsValueDefinitions) { }
    }
}
