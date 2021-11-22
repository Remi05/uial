using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows
{
    public class Resize : AbstractPatternInteraction<IUIAutomationTransformPattern>, IInteraction
    {
        public const string Key = "Resize";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_TransformPatternId;

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
