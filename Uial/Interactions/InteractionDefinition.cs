using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions
{
    public class InteractionDefinition : IInteractionDefinition
    {
        protected DefinitionScope Scope { get; set; }
        public string Name { get; protected set; }
        protected IEnumerable<string> ParamNames { get; set; }
        protected IEnumerable<IBaseInteractionDefinition> BaseInteractionDefinitions { get; set; }

        public InteractionDefinition(DefinitionScope scope, string name, IEnumerable<string> paramNames, IEnumerable<IBaseInteractionDefinition> baseInteractionDefinitions)
        {
            // throw if any param is null
            Scope = scope;
            Name = name;
            ParamNames = paramNames;
            BaseInteractionDefinitions = baseInteractionDefinitions;
        }

        public IInteraction Resolve(IContext parentContext, IInteractionProvider interactionProvider, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != ParamNames.Count())
            {
                throw new InvalidParameterCountException(ParamNames.Count(), paramValues.Count());
            }

            Dictionary<string, string> referenceValues = new Dictionary<string, string>(parentContext.Scope.ReferenceValues);
            for (int i = 0; i < ParamNames.Count(); ++i)
            {
                referenceValues[ParamNames.ElementAt(i)] = paramValues.ElementAt(i);
            }

            RuntimeScope currentScope = new RuntimeScope(Scope, referenceValues);
            IEnumerable<IInteraction> interactions = BaseInteractionDefinitions.Select((interactionDefinition) => interactionDefinition.Resolve(parentContext, interactionProvider, currentScope));
            return new CompositeInteraction(Name, interactions);
        }
    }
}
