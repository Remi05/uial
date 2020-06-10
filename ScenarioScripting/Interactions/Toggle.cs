using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public class Toggle : AbstractPatternInteraction<TogglePattern>, IInteraction
    {
        public override string Name => "Toggle";
        protected override AutomationPattern AutomationPattern => TogglePattern.Pattern;

        public override void Do()
        {
            base.Do();
            Pattern.Toggle();
        }

    }
}
