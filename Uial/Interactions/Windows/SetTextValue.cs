﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using Uial.Contexts.Windows;

namespace Uial.Interactions.Windows
{
    public class SetTextValue : AbstractPatternInteraction<ValuePattern>, IInteraction
    {
        public const string Key = "SetTextValue";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => ValuePattern.Pattern;

        private string Value { get; set; }

        public SetTextValue(IWindowsVisualContext context, string value)
            : base(context)
        {
            Value = value;
        }

        public override void Do()
        {
            base.Do();
            Pattern.SetValue(Value);
        }

        public static SetTextValue FromRuntimeValues(IWindowsVisualContext context, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            return new SetTextValue(context, paramValues.ElementAt(0));
        }
    }
}
