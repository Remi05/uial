using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using Uial.Contexts;

namespace Uial.Interactions.Core
{
    public class Scroll : AbstractPatternInteraction<ScrollPattern>, IInteraction
    {
        public const string Key = "Scroll";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => ScrollPattern.Pattern;

        private ScrollAmount HorizontalScroll { get; set; }
        private ScrollAmount VerticalScroll { get; set; }

        public Scroll(IContext context, ScrollAmount horizontalScroll, ScrollAmount verticalScroll)
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

        public static Scroll FromRuntimeValues(IContext context, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 2)
            {
                throw new InvalidParameterCountException(2, paramValues.Count());
            }
            ScrollAmount horizontalScroll = ScrollAmountFromString(paramValues.ElementAt(0));
            ScrollAmount verticalScroll = ScrollAmountFromString(paramValues.ElementAt(1));
            return new Scroll(context, horizontalScroll, verticalScroll);
        }

        private static ScrollAmount ScrollAmountFromString(string scrollAmountStr)
        {
            Dictionary<string, ScrollAmount> scrollAmountMap = new Dictionary<string, ScrollAmount>()
            {
                {  "NoAmount",       ScrollAmount.NoAmount },
                {  "LargeDecrement", ScrollAmount.LargeDecrement },
                {  "LargeIncrement", ScrollAmount.LargeIncrement },
                {  "SmallDecrement", ScrollAmount.SmallDecrement },
                {  "SmallIncrement", ScrollAmount.SmallIncrement },
            };
            if (!scrollAmountMap.ContainsKey(scrollAmountStr))
            {
                throw new ArgumentException($"\"{scrollAmountStr}\" is not a valid ScrollAmount.");
            }
            return scrollAmountMap[scrollAmountStr];
        }
    }
}
