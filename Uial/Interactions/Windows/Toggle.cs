using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows
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
