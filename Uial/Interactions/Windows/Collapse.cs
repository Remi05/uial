using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows
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
