using System;
using System.Windows.Automation;
using Uial.Contexts;

namespace Uial.Interactions.Getters
{
    public class GetRangeValue
    {
        public const string Key = "GetRangeValue";

        public string Name => Key;
        private RangeValuePattern Pattern => GetPattern(Context, RangeValuePattern.Pattern);
        private IContext Context { get; set; }
        public double Value { get; private set; }

        public GetRangeValue(IContext context)
        {
            Context = context;
        }

        public void Do()
        {
            if (!Context.IsAvailable())
            {
                throw new ContextUnavailableException(Context.Name);
            }
            if (Pattern == null)
            {
                throw new InteractionUnavailableException(Name);
            }
            Value = Pattern.Current.Value;
        }

        private static RangeValuePattern GetPattern(IContext context, AutomationPattern automationPattern)
        {
            RangeValuePattern pattern;
            try
            {
                pattern = context.RootElement.GetCurrentPattern(automationPattern) as RangeValuePattern;
            }
            catch (InvalidOperationException)
            {
                // The patten is not supported by the given context's root element.
                pattern = null;
            }
            return pattern;
        }
    }
}
