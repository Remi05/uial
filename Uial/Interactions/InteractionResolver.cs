using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Values;

namespace Uial.Interactions
{
    public class InteractionResolver : IInteractionResolver
    {
        protected IBaseInteractionResolver BaseInteractionResolver { get; set; }

        public InteractionResolver(IBaseInteractionResolver baseInteractionResolver)
        {
            BaseInteractionResolver = baseInteractionResolver;
        }

        public IInteraction Resolve(InteractionDefinition interactionDefinition, IEnumerable<object> paramValues, IContext parentContext)
        {
            if (paramValues.Count() != interactionDefinition.ParamNames.Count())
            {
                throw new InvalidParameterCountException(interactionDefinition.ParamNames.Count(), paramValues.Count());
            }

            IReferenceValueStore valueStore = parentContext.Scope.ReferenceValueStore.GetCopy();
            foreach (var param in interactionDefinition.ParamNames.Zip(paramValues))
            {
                valueStore.SetValue(param.First, param.Second);
            }

            IEnumerable<IInteraction> interactions = interactionDefinition.BaseInteractionDefinitions.Select((interactionDefinition) => BaseInteractionResolver.Resolve(interactionDefinition, parentContext, valueStore));
            return new CompositeInteraction(interactionDefinition.Name, interactions);
        }
    }
}
