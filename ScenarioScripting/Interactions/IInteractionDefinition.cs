using System.Collections.Generic;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public interface IInteractionDefinition
    {
        string Name { get; }

        IInteraction Resolve(IContext context, IEnumerable<string> paramValues);
    }
}
