using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows
{
    public class ScrollIntoView : AbstractPatternInteraction<IUIAutomationScrollItemPattern>, IInteraction
    {
        public const string Key = "ScrollIntoView";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_ScrollItemPatternId;

        public ScrollIntoView(IWindowsVisualContext context)
            : base(context)
        { }

        public override void Do()
        {
            base.Do();
            Pattern.ScrollIntoView();
        }
    }
}
