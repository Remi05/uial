using System.Windows.Automation;
using Uial.Contexts;

namespace Uial.Interactions.Core
{
    public class Collapse : AbstractPatternInteraction<ExpandCollapsePattern>, IInteraction
    {
        public const string Key = "Collapse";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => ExpandCollapsePattern.Pattern;

        public Collapse(IContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Collapse();
        }
    }
}
