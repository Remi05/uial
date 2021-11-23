using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows
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
