using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using Uial.Contexts.Windows;

namespace Uial.Interactions.Windows
{
    public class Move : AbstractPatternInteraction<TransformPattern>, IInteraction
    {
        public const string Key = "Move";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => TransformPattern.Pattern;

        private double X { get; set; }
        private double Y { get; set; }

        public Move(IWindowsVisualContext context, double x, double y) 
            : base(context) 
        {
            X = x;
            Y = y;
        }

        public override void Do()
        {
            base.Do();
            // TODO: Check that the control can be moved;
            Pattern.Move(X, Y);
        }

        public static Move FromRuntimeValues(IWindowsVisualContext context, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 2)
            {
                throw new InvalidParameterCountException(2, paramValues.Count());
            }
            double x = double.Parse(paramValues.ElementAt(0));
            double y = double.Parse(paramValues.ElementAt(1));
            return new Move(context, x, y);
        }
    }
}
