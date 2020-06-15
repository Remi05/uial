using System.Windows.Automation;
using Uial.Contexts;

namespace Uial.Interactions.Core
{
    public class Restore : AbstractPatternInteraction<WindowPattern>, IInteraction
    {
        public const string Key = "Restore";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => WindowPattern.Pattern;

        public Restore(IContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.SetWindowVisualState(WindowVisualState.Normal);
        }
    }
}
