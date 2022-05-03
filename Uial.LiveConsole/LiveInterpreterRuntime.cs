using System;
using System.Collections.Generic;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Interactions;
using UIAutomationClient;

namespace Uial.LiveConsole
{
    public class LiveInterpreterRuntime : UialRuntime
    {
        public void RunInteraction(BaseInteractionDefinition baseInteractionDefinition)
        {
            IInteraction interaction = BaseInteractionResolver.Resolve(baseInteractionDefinition, RootContext, ReferenceValueStore);
            interaction.Do();
        }

        public IContext ResolveBaseContext(BaseContextDefinition baseContextDefinition)
        {
            return BaseContextResolver.Resolve(baseContextDefinition, RootContext);
        }
    }
}
