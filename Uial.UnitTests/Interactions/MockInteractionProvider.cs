using System.Collections.Generic;
using Uial.Contexts;
using Uial.Interactions;
using Uial.Scopes;

namespace Uial.UnitTests.Interactions
{
    class MockInteractionProvider : IInteractionProvider
    {
        public IDictionary<string, IInteraction> InteractionsMap { get; protected set; } = new Dictionary<string, IInteraction>();

        public string PassedInteractionName { get; protected set; }
        public IEnumerable<object> PassedParamValues { get; protected set; }
        public IContext PassedContext { get; protected set; }

        public bool IsInteractionAvailableForContext(string interactionName, IContext context)
        {
            return InteractionsMap.ContainsKey(interactionName);
        }

        public IInteraction GetInteractionByName(string interactionName, IEnumerable<object> paramValues, IContext context)
        {
            PassedInteractionName = interactionName;
            PassedParamValues = paramValues;
            PassedContext = context;
            return InteractionsMap[interactionName];
        }
    }
}
