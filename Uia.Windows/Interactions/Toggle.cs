using UIAutomationClient;
using Uial.Interactions;
using Uial.Windows.Contexts;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Windows.Interactions
{
    public class Toggle : AbstractPatternInteraction<IUIAutomationTogglePattern>, IInteraction
    {
        public const string Key = "Toggle";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_TogglePatternId;

        public Toggle(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Toggle();
        }

    }
}
