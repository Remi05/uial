using System;
using System.Collections.Generic;
using ScenarioScripting.Interactions;

namespace ScenarioScripting
{
    public class Scenario : CompositeInteraction
    {
        public Scenario(string name, List<IInteraction> interactions)
            : base(name, interactions) { }
    }
}
