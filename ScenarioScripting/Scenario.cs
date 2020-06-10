using System;
using System.Collections.Generic;
using ScenarioScripting.Contexts;
using ScenarioScripting.Interactions;

namespace ScenarioScripting
{
    public class Scenario : CompositeInteraction
    {
        public Scenario(IContext context, string name, List<IInteraction> interactions)
            : base(context, name, interactions) { }
    }
}
