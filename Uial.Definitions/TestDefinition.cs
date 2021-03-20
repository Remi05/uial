using System;
using System.Collections.Generic;
using System.Linq;

namespace Uial.Definitions
{
    public class TestDefinition
    {
        public string Name { get; protected set; }
        protected IEnumerable<BaseInteractionDefinition> BaseInteractionDefinitions { get; set; }

        public TestDefinition(string name, IEnumerable<BaseInteractionDefinition> baseInteractionDefinitions)
        {
            if (name == null || baseInteractionDefinitions == null)
            {
                throw new ArgumentNullException(name == null ? nameof(name) : nameof(baseInteractionDefinitions));
            }
            Name = name;
            BaseInteractionDefinitions = baseInteractionDefinitions;
        }
    }
}
