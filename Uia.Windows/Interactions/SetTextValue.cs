using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Interactions;
using Uial.Windows.Contexts;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Windows.Interactions
{
    public class SetTextValue : AbstractPatternInteraction<IUIAutomationValuePattern>, IInteraction
    {
        public const string Key = "SetTextValue";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_ValuePatternId;

        private string Value { get; set; }

        public SetTextValue(IWindowsVisualContext context, string value)
            : base(context)
        {
            Value = value;
        }

        public override void Do()
        {
            base.Do();
            Pattern.SetValue(Value);
        }

        public static SetTextValue FromRuntimeValues(IWindowsVisualContext context, IEnumerable<object> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            return new SetTextValue(context, paramValues.ElementAt(0) as string);
        }
    }
}
