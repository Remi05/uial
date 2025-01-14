using UIAutomationClient;
using Uial.Interactions;
using Uial.Windows.Contexts;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Windows.Interactions
{
    public class Collapse : AbstractPatternInteraction<IUIAutomationExpandCollapsePattern>, IInteraction
    {
        public const string Key = "Collapse";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_ExpandCollapsePatternId;

        public Collapse(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Collapse();
        }
    }
}
