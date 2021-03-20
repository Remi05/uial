using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uial.Contexts;
using Uial.Interactions;
using Uial.Scopes;

namespace TestUialModule
{
    public class TestInteractionProvider : IInteractionProvider
    {
        public bool IsKnownInteraction(string interactionName)
        {
            return interactionName == nameof(TestModuleInteraction);
        }

        public IInteraction GetInteractionByName(IContext context, RuntimeScope scope, string interactionName, IEnumerable<string> paramValues)
        {
            if (!IsKnownInteraction(interactionName))
            {
                throw new InteractionUnavailableException(interactionName);
            }
            return new TestModuleInteraction();
        }
    }
}
