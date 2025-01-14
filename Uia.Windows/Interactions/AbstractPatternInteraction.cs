using System;
using UIAutomationClient;
using Uial.Contexts;
using Uial.Interactions;
using Uial.Windows.Contexts;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Windows.Interactions
{
    public abstract class AbstractPatternInteraction<T> : AbstractInteraction, IInteraction where T : class
    {
        protected abstract AutomationPatternIdentifier AutomationPattern { get; }
        protected T Pattern => GetPattern(Context, AutomationPattern);

        public AbstractPatternInteraction(IWindowsVisualContext context) : base(context) { }

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

        private static T GetPattern(IWindowsVisualContext context, AutomationPatternIdentifier automationPattern) 
        {
            try
            {
                return context.RootElement.GetCurrentPattern(automationPattern) as T;
            }
            catch (InvalidOperationException)
            {
                // The patten is not supported by the given context's root element.
                return null;
            }
        }
    }
}
