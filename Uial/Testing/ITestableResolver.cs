using Uial.Contexts;
using Uial.DataModels;

namespace Uial.Testing
{
    public interface ITestableResolver
    {
        ITestable Resolve(TestableDefinition testDefinition, IContext context);
        Test Resolve(TestDefinition testDefinition, IContext context);
        TestGroup Resolve(TestGroupDefinition testDefinition, IContext context);
    }
}