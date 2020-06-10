using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public class Select : AbstractPatternInteraction<SelectionItemPattern>, IInteraction
    {
        public override string Name => "Select";
        protected override AutomationPattern AutomationPattern => SelectionItemPattern.Pattern;

        public override void Do()
        {
            base.Do();
            Pattern.Select();
        }
    }
}
