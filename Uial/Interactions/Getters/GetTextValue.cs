using System;
using System.Windows.Automation;
using Uial.Contexts;

namespace Uial.Interactions.Getters
{
    public class GetTextValue
    {
        public const string Key = "GetTextValue";

        public string Name => Key;
        private ValuePattern Pattern => GetPattern(Context, ValuePattern.Pattern);
        private IContext Context { get; set; }
        public string Value { get; private set; }

        public GetTextValue(IContext context)
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

        private static ValuePattern GetPattern(IContext context, AutomationPattern automationPattern)
        {
            ValuePattern pattern;
            try
            {
                pattern = context.RootElement.GetCurrentPattern(automationPattern) as ValuePattern;
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
