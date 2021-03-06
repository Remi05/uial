﻿using System.Windows.Automation;
using Uial.Contexts.Windows;

namespace Uial.Interactions.Windows
{
    public class Collapse : AbstractPatternInteraction<ExpandCollapsePattern>, IInteraction
    {
        public const string Key = "Collapse";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => ExpandCollapsePattern.Pattern;

        public Collapse(IWindowsVisualContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            Pattern.Collapse();
        }
    }
}
