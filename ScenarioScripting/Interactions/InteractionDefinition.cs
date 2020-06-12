using System.Collections.Generic;
using System.Linq;
using ScenarioScripting.Contexts;
using ScenarioScripting.Scopes;

namespace ScenarioScripting.Interactions
{
    public class InteractionDefinition : IInteractionDefinition
    {
        protected DefinitionScope Scope { get; set; }
        public string Name { get; protected set; }
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

        public IInteraction Resolve(IContext parentContext, IEnumerable<object> paramValues)
        {
            if (paramValues.Count() != ParamNames.Count())
            {
                throw new InvalidParameterCountException(ParamNames.Count(), paramValues.Count());
            }

            Dictionary<string, object> referenceValues = new Dictionary<string, object>(parentContext.Scope.ReferenceValues);
            for (int i = 0; i < ParamNames.Count(); ++i)
            {
                referenceValues[ParamNames.ElementAt(i)] = paramValues.ElementAt(i);
            }

            RuntimeScope currentScope = new RuntimeScope(Scope, referenceValues);
            IEnumerable<IInteraction> interactions = BaseInteractionDefinitions.Select((interactionDefinition) => interactionDefinition.Resolve(parentContext, currentScope));
            return new CompositeInteraction(Name, interactions);
        }
    }
}
