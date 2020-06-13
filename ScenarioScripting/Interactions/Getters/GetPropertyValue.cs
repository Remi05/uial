using System;
using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions.Getters
{
    public class GetPropertyValue
    {
        public const string Key = "GetRangeValue";

        public string Name => Key;
        private IContext Context { get; set; }
        private AutomationProperty Property { get; set; }
        public object Value { get; private set; }

        public GetPropertyValue(IContext context, AutomationProperty property)
        {
            Context = context;
            Property = property;
        }

        public void Do()
        {
            if (!Context.IsAvailable())
            {
                throw new ContextUnavailableException(Context.Name);
            }
            Value = Context.RootElement.GetCurrentPropertyValue(Property);
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
