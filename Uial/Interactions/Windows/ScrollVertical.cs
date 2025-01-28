using System;
using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows
{
    public class ScrollVertical : AbstractPatternInteraction<IUIAutomationScrollPattern>, IInteraction
    {
        public const string Key = "ScrollVertical";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_ScrollPatternId;
        private ScrollAmount VerticalScroll { get; set; }

        public ScrollVertical(IWindowsVisualContext context, ScrollAmount verticalScroll)
            : base(context)
        {
            VerticalScroll = verticalScroll;
        }

        public override void Do()
        {
            base.Do();
            Pattern.Scroll(ScrollAmount.ScrollAmount_NoAmount, VerticalScroll);
        }

        public static ScrollVertical FromRuntimeValues(IWindowsVisualContext context, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            ScrollAmount verticalScroll = Scroll.ScrollAmountFromString(paramValues.ElementAt(0));
            return new ScrollVertical(context, verticalScroll);
        }
    }
}
