using System;
using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Interactions;
using Uial.Values;
using Uial.Windows.Contexts;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Windows.Interactions
{
    public class GetTextValue : AbstractPatternInteraction<IUIAutomationValuePattern>, IInteraction
    {
        public const string Key = "GetTextValue";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_ValuePatternId;

        protected string ReferenceName { get; set; }
        protected IReferenceValueStore ReferenceValueStore { get; set; }

        public GetTextValue(IWindowsVisualContext context, string referenceName, IReferenceValueStore referenceValueStore) : base(context)
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
            ReferenceValueStore.SetValue(ReferenceName, Pattern.CurrentValue);
        }

        public static GetTextValue FromRuntimeValues(IWindowsVisualContext context, IEnumerable<object> paramValues, IReferenceValueStore referenceValueStore)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            string referenceName = paramValues.ElementAt(0) as string;
            return new GetTextValue(context, referenceName, referenceValueStore);
        }
    }
}
