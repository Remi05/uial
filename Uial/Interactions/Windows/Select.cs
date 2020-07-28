using System.Windows.Automation;
using Uial.Contexts;

namespace Uial.Interactions.Windows
{
    public class Select : AbstractPatternInteraction<SelectionItemPattern>, IInteraction
    {
        public const string Key = "Select";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => SelectionItemPattern.Pattern;

        public Select(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Select();
        }
    }
}
