using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScenarioScripting.Contexts;
using ScenarioScripting.Interactions;
using ScenarioScripting.Scopes;

namespace ScenarioScriptingUnitTests.Interactions
{
    class MockBaseInteractionDefinition : IBaseInteractionDefinition
    {
        protected IInteraction Interaction { get; set; }

        public MockBaseInteractionDefinition(IInteraction interaction)
        {
            Interaction = interaction;
        }

        public IInteraction Resolve(IContext context, RuntimeScope scope)
        {
            return Interaction;
        }
    }
}
