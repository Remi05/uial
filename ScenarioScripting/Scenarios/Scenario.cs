using System.Collections.Generic;
using ScenarioScripting.Contexts;
using ScenarioScripting.Interactions;

namespace ScenarioScripting.Scenarios
{
    public class Scenario : CompositeInteraction
    {
        public Scenario(string name, IEnumerable<IInteraction> interactions)
            : base(name, interactions) { }
    }
}
