using System;
using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows
{
    public class SetVerticalScrollPercent : AbstractPatternInteraction<IUIAutomationScrollPattern>, IInteraction
    {
        public const string Key = "SetVerticalScrollPercent";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_ScrollPatternId;

        private double VerticalScrollPercent { get; set; }

        public SetVerticalScrollPercent(IWindowsVisualContext context, double verticalScrollPercent)
            : base(context)
        {
            VerticalScrollPercent = verticalScrollPercent;
        }

        public override void Do()
        {
            base.Do();
            Pattern.SetScrollPercent(0.0, VerticalScrollPercent);
        }

        public static SetVerticalScrollPercent FromRuntimeValues(IWindowsVisualContext context, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            double verticalScrollPercent = double.Parse(paramValues.ElementAt(0));
            return new SetVerticalScrollPercent(context, verticalScrollPercent);
        }
    }
}
