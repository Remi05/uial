using Uial.Contexts;

namespace Uial.Interactions
{
    public interface IInteraction
    {
        string Name { get; }
        
        void Do();
    }
}
