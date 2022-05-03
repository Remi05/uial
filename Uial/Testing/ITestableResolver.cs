using Uial.Contexts;
using Uial.DataModels;
using Uial.Values;

namespace Uial.Testing
{
    public interface ITestableResolver
    {
        ITestable Resolve(TestableDefinition testDefinition, IContext context, IReferenceValueStore referenceValueStore);
        Test Resolve(TestDefinition testDefinition, IContext context, IReferenceValueStore referenceValueStore);
        TestGroup Resolve(TestGroupDefinition testDefinition, IContext context, IReferenceValueStore referenceValueStore);
    }
}