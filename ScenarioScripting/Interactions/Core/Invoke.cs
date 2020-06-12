using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions.Core
{
    public class Invoke : AbstractPatternInteraction<InvokePattern>, IInteraction
    {
        public const string Key = "Invoke";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => InvokePattern.Pattern;

        public Invoke(IContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Invoke();
        }
    }
}
