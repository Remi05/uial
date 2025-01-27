using System;
using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows
{
    public class SetHorizontalScrollPercent : AbstractPatternInteraction<IUIAutomationScrollPattern>, IInteraction
    {
        public const string Key = "SetHorizontalScrollPercent";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_ScrollPatternId;

        private double HorizontalScrollPercent { get; set; }

        public SetHorizontalScrollPercent(IWindowsVisualContext context, double horizontalScrollPercent)
            : base(context)
        {
            HorizontalScrollPercent = horizontalScrollPercent;
        }

        public override void Do()
        {
            base.Do();
            Pattern.SetScrollPercent(HorizontalScrollPercent, 0.0);
        }

        public static SetHorizontalScrollPercent FromRuntimeValues(IWindowsVisualContext context, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            double horizontalScrollPercent = double.Parse(paramValues.ElementAt(0));
            return new SetHorizontalScrollPercent(context, horizontalScrollPercent);
        }
    }
}
