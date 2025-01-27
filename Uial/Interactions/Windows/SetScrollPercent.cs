using System;
using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows
{
    public class SetScrollPercent : AbstractPatternInteraction<IUIAutomationScrollPattern>, IInteraction
    {
        public const string Key = "SetScrollPercent";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_ScrollPatternId;

        private double HorizontalScrollPercent { get; set; }
        private double VerticalScrollPercent { get; set; }

        public SetScrollPercent(IWindowsVisualContext context, double horizontalScrollPercent, double verticalScrollPercent)
            : base(context)
        {
            HorizontalScrollPercent = horizontalScrollPercent;
            VerticalScrollPercent = verticalScrollPercent;
        }

        public override void Do()
        {
            base.Do();
            Pattern.SetScrollPercent(HorizontalScrollPercent, VerticalScrollPercent);
        }

        public static SetScrollPercent FromRuntimeValues(IWindowsVisualContext context, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 2)
            {
                throw new InvalidParameterCountException(2, paramValues.Count());
            }
            double horizontalScrollPercent = double.Parse(paramValues.ElementAt(0));
            double verticalScrollPercent = double.Parse(paramValues.ElementAt(1));
            return new SetScrollPercent(context, horizontalScrollPercent, verticalScrollPercent);
        }
    }
}
