using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public class CloseWindow : AbstractPatternInteraction<WindowPattern>, IInteraction
    {
        public const string Key = "CloseWindow";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => WindowPattern.Pattern;

        public CloseWindow(IContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Close();
        }
    }
}
