using Uial.Contexts;
using Uial.DataModels;
using Uial.Values;

namespace Uial.Scenarios
{
    public interface IScenarioResolver
    {
        Scenario Resolve(ScenarioDefinition scenarioDefinition, IContext context, IReferenceValueStore referenceValueStore);
    }
}
