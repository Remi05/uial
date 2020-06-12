using ScenarioScripting.Contexts;
using ScenarioScripting.Scopes;

namespace ScenarioScripting.Scenarios
{
    public interface IScenarioDefinition
    {
        string Name { get; }
        Scenario Resolve(IContext context);
    }
}
