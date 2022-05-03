using UIAutomationClient;
using Uial.Interactions;
using Uial.Windows.Contexts;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Windows.Interactions
{
    public class Restore : AbstractPatternInteraction<IUIAutomationWindowPattern>, IInteraction
    {
        public const string Key = "Restore";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_WindowPatternId;

        public Restore(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.SetWindowVisualState(WindowVisualState.WindowVisualState_Normal);
        }
    }
}
