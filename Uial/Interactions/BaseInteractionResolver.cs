using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Values;

namespace Uial.Interactions
{
    public class BaseInteractionResolver : IBaseInteractionResolver
    {
        protected IInteractionProvider InteractionProvider { get; set; }
        protected IBaseContextResolver BaseContextResolver { get; set; }
        protected IValueResolver ValueResolver { get; set; }

        public BaseInteractionResolver(IInteractionProvider interactionProvider, IBaseContextResolver baseContextResolver, IValueResolver valueResolver)
        {
            InteractionProvider = interactionProvider;
            BaseContextResolver = baseContextResolver;
            ValueResolver = valueResolver;
        }

        public IInteraction Resolve(BaseInteractionDefinition baseInteractionDefinition, IContext parentContext, IReferenceValueStore referenceValueStore)
        {
            if (baseInteractionDefinition == null)
            {
                throw new ArgumentNullException(nameof(baseInteractionDefinition));
            }
            BaseContextDefinition baseContextDefinition = baseInteractionDefinition.ContextDefinition;
            IContext context = baseContextDefinition == null ? null : BaseContextResolver.Resolve(baseContextDefinition, parentContext) ?? parentContext;

            var paramValues = new List<object>();
            if (baseInteractionDefinition.ParamsValueDefinitions != null)
            {
                foreach (var paramValueDefinition in baseInteractionDefinition.ParamsValueDefinitions)
                {
                    object paramValue = ValueResolver.Resolve(paramValueDefinition, referenceValueStore);
                    paramValues.Add(paramValue);
                }
            }      
            
            return InteractionProvider.GetInteractionByName(baseInteractionDefinition.InteractionName, paramValues, context);
        }
    }
}
