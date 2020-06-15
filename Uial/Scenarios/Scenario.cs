using System.Collections.Generic;
using Uial.Contexts;
using Uial.Interactions;

namespace Uial.Scenarios
{
    public class Scenario : CompositeInteraction
    {
        public Scenario(string name, IEnumerable<IInteraction> interactions)
            : base(name, interactions) { }
    }
}
