using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions.Core
{
    public class SetRangeValue : AbstractPatternInteraction<RangeValuePattern>, IInteraction
    {
        public const string Key = "SetRangeValue";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => RangeValuePattern.Pattern;

        public double Value { get; set; }

        public SetRangeValue(IContext context, double value)
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

        public static SetRangeValue FromRuntimeValues(IContext context, IEnumerable<object> paramValues)
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
