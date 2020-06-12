﻿using System.Windows.Automation;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions.Core
{
    public class Minimize : AbstractPatternInteraction<WindowPattern>, IInteraction
    {
        public const string Key = "Minimize";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => WindowPattern.Pattern;

        public Minimize(IContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            // TODO: Check that the window can be minimized;
            Pattern.SetWindowVisualState(WindowVisualState.Minimized);
        }
    }
}
