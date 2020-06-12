using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions.Core
{
    public class Focus : IInteraction
    {
        public const string Key = "Focus";

        public string Name => Key;

        private IContext Context { get; set; }

        public Focus(IContext context)
        {
            Context = context;
        }

        public void Do()
        {
            Context.RootElement.SetFocus();
        }
    }
}
