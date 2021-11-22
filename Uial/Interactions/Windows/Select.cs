using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows
{
    public class Select : AbstractPatternInteraction<IUIAutomationSelectionItemPattern>, IInteraction
    {
        public const string Key = "Select";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_SelectionItemPatternId;

        public Select(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Select();
        }
    }
}
