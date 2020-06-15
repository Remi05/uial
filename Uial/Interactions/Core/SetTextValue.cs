using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using Uial.Contexts;

namespace Uial.Interactions.Core
{
    public class SetTextValue : AbstractPatternInteraction<ValuePattern>, IInteraction
    {
        public const string Key = "SetTextValue";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => ValuePattern.Pattern;

        private string Value { get; set; }

        public SetTextValue(IContext context, string value)
            : base(context)
        {
            Value = value;
        }

        public override void Do()
        {
            base.Do();
            Pattern.SetValue(Value);
        }

        public static SetTextValue FromRuntimeValues(IContext context, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            return new SetTextValue(context, paramValues.ElementAt(0));
        }
    }
}
