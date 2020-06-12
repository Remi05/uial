using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions.Core
{
    public class Focus : AbstractInteraction, IInteraction
    {
        public const string Key = "Focus";

        public override string Name => Key;

        public Focus(IContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Context.RootElement.SetFocus();
        }
    }
}
