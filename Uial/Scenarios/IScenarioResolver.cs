using Uial.Contexts;
using Uial.Definitions;
using Uial.Interactions;

namespace Uial.Scenarios
{
    public interface IScenarioResolver
    {
        Scenario Resolve(ScenarioDefinition scenarioDefinition, IContext context, IInteractionProvider interactionProvider);
    }
}
