
namespace Uial.Testing
{
    public interface ITestable
    {
        string Name { get; }

        ITestResults RunTest();
    }
}
