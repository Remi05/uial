using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public class SetValue : AbstractPatternInteraction<ValuePattern>, IInteraction
    {
        public const string Key = "SetValue";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => ValuePattern.Pattern;

        public string Value { get; set; }

        public SetValue(IContext context, string value)
            : base(context)
        {
            Value = value;
        }

        public override void Do()
        {
            base.Do();
            Pattern.SetValue(Value);
        }

        public static SetValue FromRuntimeValues(IContext context, IEnumerable<object> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            return new SetValue(context, paramValues.ElementAt(0) as string);
        }
    }
}
