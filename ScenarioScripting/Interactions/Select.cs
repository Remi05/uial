using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public class Select : AbstractPatternInteraction<SelectionItemPattern>, IInteraction
    {
        public const string Key = "Select";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => SelectionItemPattern.Pattern;

        public Select(IContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Select();
        }
    }
}
