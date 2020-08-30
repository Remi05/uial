using Uial.Contexts;
using Uial.Interactions;

namespace Uial.Scenarios
{
    public interface IScenarioDefinition
    {
        string Name { get; }
        Scenario Resolve(IContext context, IInteractionProvider interactionProvider);
    }
}
