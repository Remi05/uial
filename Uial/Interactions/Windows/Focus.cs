using Uial.Contexts.Windows;

namespace Uial.Interactions.Windows
{
    public class Focus : AbstractInteraction, IInteraction
    {
        public const string Key = "Focus";

        public override string Name => Key;

        public Focus(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Context.RootElement.SetFocus();
        }
    }
}
