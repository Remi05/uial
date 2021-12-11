using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Interactions;
using Uial.Windows.Contexts;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Windows.Interactions
{
    public class SetRangeValue : AbstractPatternInteraction<IUIAutomationRangeValuePattern>, IInteraction
    {
        public const string Key = "SetRangeValue";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_RangeValuePatternId;

        private double Value { get; set; }

        public SetRangeValue(IWindowsVisualContext context, double value)
            : base(context)
        {
            Value = value;
        }

        public override void Do()
        {
            base.Do();
            // TODO: Add check for range bounds.
            Pattern.SetValue(Value);
        }

        public static SetRangeValue FromRuntimeValues(IWindowsVisualContext context, IEnumerable<object> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            double rangeValue = double.Parse(paramValues.ElementAt(0) as string);
            return new SetRangeValue(context, rangeValue);
        }
    }
}
