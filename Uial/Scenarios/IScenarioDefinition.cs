using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Scenarios
{
    public interface IScenarioDefinition
    {
        string Name { get; }
        Scenario Resolve(IContext context);
    }
}
