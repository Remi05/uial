﻿using System.Windows.Automation;
using Uial.Contexts.Windows;

namespace Uial.Interactions.Windows
{
    public class Toggle : AbstractPatternInteraction<TogglePattern>, IInteraction
    {
        public const string Key = "Toggle";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => TogglePattern.Pattern;

        public Toggle(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Toggle();
        }

    }
}
