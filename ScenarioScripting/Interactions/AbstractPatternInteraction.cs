using System;
using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public abstract class AbstractPatternInteraction<T> : AbstractInteraction, IInteraction where T : BasePattern
    {
        protected abstract AutomationPattern AutomationPattern { get; }
        protected T Pattern => GetPattern(Context, AutomationPattern);

        public AbstractPatternInteraction(IContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            if (!Context.IsAvailable())
            {
                throw new ContextUnavailableException(Context.Name);
            }
            if (Pattern == null)
            {
                throw new InteractionUnavailableException(Name);
            }
        }

        private static T GetPattern(IContext context, AutomationPattern automationPattern) 
        {
            T pattern;
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
