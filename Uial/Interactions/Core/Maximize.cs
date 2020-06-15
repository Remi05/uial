using System.Windows.Automation;
using Uial.Contexts;

namespace Uial.Interactions.Core
{
    public class Maximize : AbstractPatternInteraction<WindowPattern>, IInteraction
    {
        public const string Key = "Maximize";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => WindowPattern.Pattern;

        public Maximize(IContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            // TODO: Check that the window can be maximized;
            Pattern.SetWindowVisualState(WindowVisualState.Maximized);
        }
    }
}
