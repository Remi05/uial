﻿using System.Windows.Automation;
using Uial.Contexts.Windows;

namespace Uial.Interactions.Windows
{
    public class Restore : AbstractPatternInteraction<WindowPattern>, IInteraction
    {
        public const string Key = "Restore";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => WindowPattern.Pattern;

        public Restore(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.SetWindowVisualState(WindowVisualState.Normal);
        }
    }
}
