﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using Uial.Contexts.Windows;

namespace Uial.Interactions.Windows
{
    public class Resize : AbstractPatternInteraction<TransformPattern>, IInteraction
    {
        public const string Key = "Resize";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => TransformPattern.Pattern;

        private double Width { get; set; }
        private double Height { get; set; }

        public Resize(IWindowsVisualContext context, double width, double height) 
            : base(context) 
        {
            Width = width;
            Height = height;
        }

        public override void Do()
        {
            base.Do();
            // TODO: Check that the control can be resized;
            Pattern.Resize(Width, Height);
        }

        public static Resize FromRuntimeValues(IWindowsVisualContext context, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 2)
            {
                throw new InvalidParameterCountException(2, paramValues.Count());
            }
            double width = double.Parse(paramValues.ElementAt(0));
            double height = double.Parse(paramValues.ElementAt(1));
            return new Resize(context, width, height);
        }
    }
}
