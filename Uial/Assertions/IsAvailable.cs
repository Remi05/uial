using Uial.Contexts;

namespace Uial.Assertions
{
    public class IsAvailable : IAssertion
    {
        public string Name => "IsAvailable";

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
