using System;
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
            if (interactionDefinition == null || parentContext == null)
            {
                throw new ArgumentNullException(interactionDefinition == null ? nameof(interactionDefinition) : nameof(parentContext));
            }

            int paramNamesCount = (interactionDefinition.ParamNames?.Count()).GetValueOrDefault(0);
            int paramValuesCount = (paramValues?.Count()).GetValueOrDefault(0);
            if (paramValuesCount != paramNamesCount)
            {
                throw new InvalidParameterCountException(paramNamesCount, paramValuesCount);
            }

            var interactions = new List<IInteraction>();
            if (interactionDefinition.BaseInteractionDefinitions != null && interactionDefinition.BaseInteractionDefinitions.Count() > 0)
            {
                IReferenceValueStore valueStore = parentContext.Scope.ReferenceValueStore.GetCopy();
                if (paramValuesCount != 0)
                {
                    foreach (var param in interactionDefinition.ParamNames.Zip(paramValues))
                    {
                        valueStore.SetValue(param.First, param.Second);
                    }
                }

                foreach (BaseInteractionDefinition baseInteractionDefinition in interactionDefinition.BaseInteractionDefinitions)
                {
                    IInteraction interaction = BaseInteractionResolver.Resolve(baseInteractionDefinition, parentContext, valueStore);
                    interactions.Add(interaction);
                }
            }

            return new CompositeInteraction(interactionDefinition.Name, interactions);
        }
    }
}
