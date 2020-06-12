
namespace ScenarioScripting.Assertions
{
    public interface IAssertion
    {
        string Name { get; }

        bool Assert();
    }
}
