using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public class Select : AbstractPatternInteraction<SelectionItemPattern>, IInteraction
    {
        public override string Name => "Select";
        protected override AutomationPattern AutomationPattern => SelectionItemPattern.Pattern;

        public override void Do(IContext context)
        {
            SelectionItemPattern pattern = GetPattern(context);
            if (pattern == null)
            {
                throw new InteractionUnavailableException();
            }
            pattern.Select();
        }
    }
}
