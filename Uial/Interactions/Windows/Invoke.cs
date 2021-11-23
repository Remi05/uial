using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows
{
    public class Invoke : AbstractPatternInteraction<IUIAutomationInvokePattern>, IInteraction
    {
        public const string Key = "Invoke";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_InvokePatternId;

        public Invoke(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Invoke();
        }
    }
}
