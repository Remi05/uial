using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Interactions;
using Uial.Windows.Contexts;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Windows.Interactions
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

        public static Resize FromRuntimeValues(IWindowsVisualContext context, IEnumerable<object> paramValues)
        {
            if (paramValues.Count() != 2)
            {
                throw new InvalidParameterCountException(2, paramValues.Count());
            }
            double width = double.Parse(paramValues.ElementAt(0) as string);
            double height = double.Parse(paramValues.ElementAt(1) as string);
            return new Resize(context, width, height);
        }
    }
}
