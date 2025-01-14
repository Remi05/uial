using System;
using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Interactions;
using Uial.Values;
using Uial.Windows.Contexts;

namespace Uial.Windows.Interactions
{
    public class GetRangeValue : AbstractPatternInteraction<IUIAutomationRangeValuePattern>, IInteraction
    {
        public const string Key = "GetRangeValue";

        public override string Name => Key;
        protected override int AutomationPattern => UIA_PatternIds.UIA_RangeValuePatternId;
        
        protected string ReferenceName { get; set; }
        protected IReferenceValueStore ReferenceValueStore { get; set; }

        public GetRangeValue(IWindowsVisualContext context, string referenceName, IReferenceValueStore referenceValueStore) : base(context)
        {
            if (referenceName == null || referenceValueStore == null)
            {
                throw new ArgumentNullException(referenceName == null ? nameof(referenceName) : nameof(referenceValueStore));
            }
            ReferenceName = referenceName;
            ReferenceValueStore = referenceValueStore;
        }

        public override void Do()
        {
            base.Do();
            ReferenceValueStore.SetValue(ReferenceName, Pattern.CurrentValue.ToString());
        }

        public static GetRangeValue FromRuntimeValues(IWindowsVisualContext context, IEnumerable<object> paramValues, IReferenceValueStore referenceValueStore)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            string referenceName = paramValues.ElementAt(0) as string;
            return new GetRangeValue(context, referenceName, referenceValueStore);
        }
    }
}
