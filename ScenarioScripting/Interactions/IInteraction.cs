using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public interface IInteraction
    {
        string Name { get; }
        
        void Do();
    }
}
