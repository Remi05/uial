using System;
using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Interactions;
using Uial.Windows.Contexts;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Windows.Interactions
{
    public class Scroll : AbstractPatternInteraction<IUIAutomationScrollPattern>, IInteraction
    {
        public const string Key = "Scroll";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_ScrollPatternId;

        private ScrollAmount HorizontalScroll { get; set; }
        private ScrollAmount VerticalScroll { get; set; }

        public Scroll(IWindowsVisualContext context, ScrollAmount horizontalScroll, ScrollAmount verticalScroll)
            : base(context)
        {
            HorizontalScroll = horizontalScroll;
            VerticalScroll = verticalScroll;
        }

        public override void Do()
        {
            base.Do();
            Pattern.Scroll(HorizontalScroll, VerticalScroll);
        }

        public static Scroll FromRuntimeValues(IWindowsVisualContext context, IEnumerable<object> paramValues)
        {
            if (paramValues.Count() != 2)
            {
                throw new InvalidParameterCountException(2, paramValues.Count());
            }
            ScrollAmount horizontalScroll = ScrollAmountFromString(paramValues.ElementAt(0) as string);
            ScrollAmount verticalScroll = ScrollAmountFromString(paramValues.ElementAt(1) as string);
            return new Scroll(context, horizontalScroll, verticalScroll);
        }

        private static ScrollAmount ScrollAmountFromString(string scrollAmountStr)
        {
            Dictionary<string, ScrollAmount> scrollAmountMap = new Dictionary<string, ScrollAmount>()
            {
                {  "NoAmount",       ScrollAmount.ScrollAmount_NoAmount },
                {  "LargeDecrement", ScrollAmount.ScrollAmount_LargeDecrement },
                {  "LargeIncrement", ScrollAmount.ScrollAmount_LargeIncrement },
                {  "SmallDecrement", ScrollAmount.ScrollAmount_SmallDecrement },
                {  "SmallIncrement", ScrollAmount.ScrollAmount_SmallIncrement },
            };
            if (!scrollAmountMap.ContainsKey(scrollAmountStr))
            {
                throw new ArgumentException($"\"{scrollAmountStr}\" is not a valid ScrollAmount.");
            }
            return scrollAmountMap[scrollAmountStr];
        }
    }
}
