using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Definitions
{
    public class InteractionDefinition
    {
        public string Name { get; protected set; }
        protected DefinitionScope Scope { get; set; }
        protected IEnumerable<string> ParamNames { get; set; }
        protected IEnumerable<BaseInteractionDefinition> BaseInteractionDefinitions { get; set; }

        public InteractionDefinition(DefinitionScope scope, string name, IEnumerable<string> paramNames, IEnumerable<BaseInteractionDefinition> baseInteractionDefinitions)
        {
            // throw if any param is null
            Scope = scope;
            Name = name;
            ParamNames = paramNames;
            BaseInteractionDefinitions = baseInteractionDefinitions;
        }
    }
}
