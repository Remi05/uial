﻿using System;
using System.Windows.Automation;
using Uial.Contexts.Windows;
using Uial.Contexts;

namespace Uial.Interactions.Windows
{
    public abstract class AbstractPatternInteraction<T> : AbstractInteraction, IInteraction where T : BasePattern
    {
        protected abstract AutomationPattern AutomationPattern { get; }
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

        private static T GetPattern(IWindowsVisualContext context, AutomationPattern automationPattern) 
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
