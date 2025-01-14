using Uial.DataModels;

namespace Uial.Contexts
{
    public interface IStateProvider
    {
        IState GetStateFromDefinition(StateDefinition stateDefinition);
    }
}
