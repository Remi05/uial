using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions.Core
{
    public class Close : AbstractPatternInteraction<WindowPattern>, IInteraction
    {
        public const string Key = "Close";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => WindowPattern.Pattern;

        public Close(IContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Close();
        }
    }
}
