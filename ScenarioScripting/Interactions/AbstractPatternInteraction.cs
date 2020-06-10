using System;
using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public abstract class AbstractPatternInteraction<T> : IInteraction where T : BasePattern
    {
        public abstract string Name { get; }

        protected abstract AutomationPattern AutomationPattern { get; }

        protected IContext Context { get; set; }

        protected T Pattern => GetPattern(Context, AutomationPattern);

        public virtual void Do()
        {
            if (Pattern == null)
            {
                throw new InteractionUnavailableException();
            }
        }

        private static T GetPattern(IContext context, AutomationPattern automationPattern) 
        {
            T pattern = null;
            try
            {
                pattern = context.RootElement.GetCurrentPattern(automationPattern) as T;
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
