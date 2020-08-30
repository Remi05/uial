using System.Windows.Automation;
using Uial.Contexts.Windows;

namespace Uial.Interactions.Windows
{
    public class Close : AbstractPatternInteraction<WindowPattern>, IInteraction
    {
        public const string Key = "Close";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => WindowPattern.Pattern;

        public Close(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Close();
        }
    }
}
