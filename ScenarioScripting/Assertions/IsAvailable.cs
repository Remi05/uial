using ScenarioScripting.Contexts;

namespace ScenarioScripting.Assertions
{
    public class IsAvailable : IAssertion
    {
        private IContext Context { get; set; }

        public IsAvailable(IContext context)
        {
            Context = context;
        }

        public bool Assert()
        {
            return Context.IsAvailable();
        }
    }
}
