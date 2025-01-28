using System;
using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows
{
    public class ScrollHorizontal : AbstractPatternInteraction<IUIAutomationScrollPattern>, IInteraction
    {
        public const string Key = "ScrollHorizontal";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_ScrollPatternId;
        private ScrollAmount HorizontalScroll { get; set; }

        public ScrollHorizontal(IWindowsVisualContext context, ScrollAmount horizontalScroll)
            : base(context)
        {
            HorizontalScroll = horizontalScroll;
        }

        public override void Do()
        {
            base.Do();
            Pattern.Scroll(HorizontalScroll, ScrollAmount.ScrollAmount_NoAmount);
        }

        public static ScrollHorizontal FromRuntimeValues(IWindowsVisualContext context, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            ScrollAmount horizontalScroll = Scroll.ScrollAmountFromString(paramValues.ElementAt(0));
            return new ScrollHorizontal(context, horizontalScroll);
        }
    }
}
