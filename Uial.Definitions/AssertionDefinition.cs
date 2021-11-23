using System.Collections.Generic;

namespace Uial.Definitions
{
    public class AssertionDefinition
    {
        public string AssertionName { get; private set; }

        public IEnumerable<ReferenceValueDefinition> ParamsValueDefinitions { get; private set; }

        public AssertionDefinition(string assertionName, IEnumerable<ReferenceValueDefinition> paramsValueDefinitions)
        {
            AssertionName = assertionName;
            ParamsValueDefinitions = paramsValueDefinitions;
        }
    }
}
