using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows
{
    public class Minimize : AbstractPatternInteraction<IUIAutomationWindowPattern>, IInteraction
    {
        public const string Key = "Minimize";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_WindowPatternId;

        public Minimize(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            // TODO: Check that the window can be minimized;
            Pattern.SetWindowVisualState(WindowVisualState.WindowVisualState_Minimized);
        }
    }
}
