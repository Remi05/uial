using UIAutomationClient;
using Uial.Interactions;
using Uial.Windows.Contexts;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Windows.Interactions
{
    public class Expand : AbstractPatternInteraction<IUIAutomationExpandCollapsePattern>, IInteraction
    {
        public const string Key = "Expand";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_ExpandCollapsePatternId;

        public Expand(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Expand();
        }
    }
}
