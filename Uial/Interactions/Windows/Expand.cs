using System.Windows.Automation;
using Uial.Contexts;

namespace Uial.Interactions.Windows
{
    public class Expand : AbstractPatternInteraction<ExpandCollapsePattern>, IInteraction
    {
        public const string Key = "Expand";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => ExpandCollapsePattern.Pattern;

        public Expand(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Expand();
        }
    }
}
