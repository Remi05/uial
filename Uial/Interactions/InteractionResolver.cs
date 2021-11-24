using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Definitions;
using Uial.Interactions;
using Uial.Scopes;

namespace Uial.Interactions
{
    public class InteractionResolver
    {
        private IBaseInteractionResolver BaseInteractionResolver { get; set; }

        public InteractionResolver(IBaseInteractionResolver baseInteractionResolver)
        {
            BaseInteractionResolver = baseInteractionResolver;
        }

        public IInteraction Resolve(InteractionDefinition interactionDefinition, IContext parentContext, IInteractionProvider interactionProvider, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != interactionDefinition.ParamNames.Count())
            {
                throw new InvalidParameterCountException(interactionDefinition.ParamNames.Count(), paramValues.Count());
            }

            Dictionary<string, string> referenceValues = new Dictionary<string, string>(parentContext.Scope.ReferenceValues);
            for (int i = 0; i < interactionDefinition.ParamNames.Count(); ++i)
            {
                referenceValues[interactionDefinition.ParamNames.ElementAt(i)] = paramValues.ElementAt(i);
            }

            RuntimeScope currentScope = new RuntimeScope(interactionDefinition.Scope, referenceValues);
            IEnumerable<IInteraction> interactions = interactionDefinition.BaseInteractionDefinitions.Select((interactionDefinition) => BaseInteractionResolver.Resolve(interactionDefinition, parentContext, interactionProvider, currentScope));
            return new CompositeInteraction(interactionDefinition.InteractionName, interactions);
        }
    }
}
