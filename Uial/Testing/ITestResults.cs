
namespace Uial.Testing
{
    public interface ITestResults
    {
        string TestName { get; }
        
        bool Passed { get; }
    }
}
