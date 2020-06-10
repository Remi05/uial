using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public class Invoke : AbstractPatternInteraction<InvokePattern>, IInteraction
    {
        public override string Name => "Invoke";
        protected override AutomationPattern AutomationPattern => InvokePattern.Pattern;

        public override void Do(IContext context)
        {
            InvokePattern pattern = GetPattern(context);
            if (pattern == null)
            {
                throw new InteractionUnavailableException();
            }
            pattern.Invoke();
        }
    }
}
