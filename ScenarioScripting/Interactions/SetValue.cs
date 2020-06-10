using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public class SetValue : AbstractPatternInteraction<ValuePattern>, IInteraction
    {
        public override string Name => "SetValue";
        protected override AutomationPattern AutomationPattern => ValuePattern.Pattern;

        public string Value { get; set; }

        public SetValue(string value)
        {
            Value = value;
        }

        public override void Do()
        {
            base.Do();
            Pattern.SetValue(Value);
        }
    }
}
