using System;
using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public abstract class AbstractPatternInteraction<T> : IInteraction where T : BasePattern
    {
        public abstract string Name { get; }

        protected abstract AutomationPattern AutomationPattern { get; }

        public abstract void Do(IContext context);

        public virtual bool IsAvailable(IContext context)
        {
            return GetPattern(context) != null;
        }

        protected T GetPattern(IContext context) 
        {
            T pattern = null;
            try
            {
                pattern = context.RootElement.GetCurrentPattern(AutomationPattern) as T;
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
