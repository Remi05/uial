using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions.Core
{
    public class Toggle : AbstractPatternInteraction<TogglePattern>, IInteraction
    {
        public const string Key = "Toggle";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => TogglePattern.Pattern;

        public Toggle(IContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Toggle();
        }

    }
}
