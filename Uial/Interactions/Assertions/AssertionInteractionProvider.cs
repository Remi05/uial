using System.Collections.Generic;
using Uial.Contexts;
using Uial.Scopes;
using Uial.Assertions;

namespace Uial.Interactions.Assertions
{
    public class AssertionInteractionProvider : IInteractionProvider
    {
        protected delegate IAssertion AssertionFactory(IEnumerable<string> paramValues);

        protected IDictionary<string, AssertionFactory> KnownAssertions = new Dictionary<string, AssertionFactory>()
        {
            { AreEqual.Key,   (paramValues) => AreEqual.FromRuntimeValues(paramValues) },
            { Contains.Key,   (paramValues) => Contains.FromRuntimeValues(paramValues) },
            { IsFalse.Key,    (paramValues) => IsFalse.FromRuntimeValues(paramValues) },
            { IsTrue.Key,     (paramValues) => IsTrue.FromRuntimeValues(paramValues) },
            { StartsWith.Key, (paramValues) => StartsWith.FromRuntimeValues(paramValues) },
        };

        public bool IsKnownInteraction(string interactionName)
        {
            return KnownAssertions.ContainsKey(interactionName);
        }

        public IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues)
        {
            if (!IsKnownInteraction(interactionName))
            {
                throw new InteractionUnavailableException(interactionName);
            }
            return new AssertionInteraction(KnownAssertions[interactionName](paramValues));
        }
    }
}

