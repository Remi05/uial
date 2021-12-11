using Uial.Contexts;
using Uial.DataModels;

namespace Uial.Scenarios
{
    public interface IScenarioResolver
    {
        Scenario Resolve(ScenarioDefinition scenarioDefinition, IContext context);
    }
}
