using Uial.DataModels;
using Uial.Values;

namespace Uial
{
    public interface IValueResolver
    {
        object Resolve(ValueDefinition valueDefintion, IReferenceValueStore referenceValueStore);
    }
}
