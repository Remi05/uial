using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public class Scroll : AbstractPatternInteraction<ScrollPattern>, IInteraction
    {
        public override string Name => "Scroll";
        protected override AutomationPattern AutomationPattern => ScrollPattern.Pattern;

        private ScrollAmount HorizontalScroll { get; set; }
        private ScrollAmount VerticalScroll { get; set; }

        public Scroll(ScrollAmount horizontalScroll, ScrollAmount verticalScroll)
        {
            HorizontalScroll = horizontalScroll;
            VerticalScroll = verticalScroll;
        }

        public override void Do(IContext context)
        {
            ScrollPattern pattern = GetPattern(context);
            if (pattern == null)
            {
                throw new InteractionUnavailableException();
            }
            pattern.Scroll(HorizontalScroll, VerticalScroll);
        }
    }
}
