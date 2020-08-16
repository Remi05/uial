using Uial.Conditions;
using Uial.Contexts;
using Uial.Interactions;
using Uial.Scenarios;

namespace Uial.Recorder
{
    public interface IScriptRecorder
    {
        IConditionDefinition GetCurrentCondition();
        IBaseContextDefinition GetCurrentBaseContext();
        IContextDefinition GetCurrentContext();

        IBaseInteractionDefinition RecordBaseInteraction();
        IInteractionDefinition RecordInteraction();
        IScenarioDefinition RecordScenario();
    }
}
