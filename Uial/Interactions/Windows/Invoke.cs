using System.Windows.Automation;
using Uial.Contexts;

namespace Uial.Interactions.Windows
{
    public class Invoke : AbstractPatternInteraction<InvokePattern>, IInteraction
    {
        public const string Key = "Invoke";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => InvokePattern.Pattern;

        public Invoke(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Invoke();
        }
    }
}
