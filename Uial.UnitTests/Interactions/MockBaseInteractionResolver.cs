using System;
using System.Collections.Generic;
using System.Text;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Interactions;
using Uial.Values;

namespace Uial.UnitTests.Interactions
{
    public class MockBaseInteractionResolver : IBaseInteractionResolver
    {
        public IDictionary<BaseInteractionDefinition, IInteraction> InteractionsMap { get; protected set; } = new Dictionary<BaseInteractionDefinition, IInteraction>();

        public List<BaseInteractionDefinition> ResolvedBaseInteractions { get; private set; } = new List<BaseInteractionDefinition>();
        public IContext LastPassedParentContext { get; private set; }
        public IReferenceValueStore LastPassedValueStore { get; private set; }

        public IInteraction Resolve(BaseInteractionDefinition baseInteractionDefinition, IContext parentContext, IReferenceValueStore referenceValueStore)
        {
            ResolvedBaseInteractions.Add(baseInteractionDefinition);
            LastPassedParentContext = parentContext;
            LastPassedValueStore = referenceValueStore;
            return InteractionsMap.ContainsKey(baseInteractionDefinition) ? InteractionsMap[baseInteractionDefinition] : null;
        }
    }
}
