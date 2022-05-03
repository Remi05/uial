using Uial.Contexts;
using Uial.DataModels;
using Uial.Values;

namespace Uial.Interactions
{
    public interface IBaseInteractionResolver
    {
        IInteraction Resolve(BaseInteractionDefinition baseInteractionDefinition, IContext parentContext, IReferenceValueStore referenceValueStore);
    }
}