using System.Collections.Generic;
using Uial.Contexts;
using Uial.Interactions;
using Uial.Scopes;

namespace Uial.UnitTests.Interactions
{
    class MockInteractionProvider : IInteractionProvider
    {
        protected IDictionary<string, IInteraction> KnownInteractions { get; set; }
         
        public MockInteractionProvider(IDictionary<string, IInteraction> knownInteractions)
        {
            KnownInteractions = knownInteractions;
        }

        public bool IsKnownInteraction(string interactionName)
        {
            return KnownInteractions.ContainsKey(interactionName);
        }

        public IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues)
        {
            return KnownInteractions[interactionName];
        }
    }
}
