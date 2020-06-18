using Uial.Contexts;

namespace Uial.Testing
{
    public interface ITestableDefinition
    {
        string Name { get; }

        ITestable Resolve(IContext context);
    }
}
