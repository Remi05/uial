using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public class CloseWindow : AbstractPatternInteraction<WindowPattern>, IInteraction
    {
        public override string Name => "CloseWindow";
        protected override AutomationPattern AutomationPattern => WindowPattern.Pattern;

        public override void Do()
        {
            base.Do();
            Pattern.Close();
        }
    }
}
