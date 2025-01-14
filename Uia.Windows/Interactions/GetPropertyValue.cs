using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Interactions;
using Uial.Values;
using Uial.Windows.Conditions;
using Uial.Windows.Contexts;

using AutomationPropertyIdentifier = System.Int32;

namespace Uial.Windows.Interactions
{
    public class GetPropertyValue : AbstractInteraction, IInteraction
    {
        public const string Key = "GetPropertyValue";

        public override string Name => Key;

        protected AutomationPropertyIdentifier Property { get; set; }
        protected string ReferenceName { get; set; }
        protected IReferenceValueStore ReferenceValueStore { get; set; }

        public GetPropertyValue(IWindowsVisualContext context, AutomationPropertyIdentifier property, string referenceName, IReferenceValueStore referenceValueStore) : base(context)
        {
            if (referenceName == null || referenceValueStore == null)
            {
                throw new ArgumentNullException(referenceName == null ? nameof(referenceName) : nameof(referenceValueStore));
            }
            Property = property;
            ReferenceName = referenceName;
            ReferenceValueStore = referenceValueStore;
        }

        public override void Do()
        {
            base.Do();
            object propertyValue = Context.RootElement.GetCurrentPropertyValue(Property);
            ReferenceValueStore.SetValue(ReferenceName, propertyValue?.ToString());
        }

        public static GetPropertyValue FromRuntimeValues(IWindowsVisualContext context, IEnumerable<object> paramValues, IReferenceValueStore referenceValueStore)
        {
            if (paramValues.Count() != 2)
            {
                throw new InvalidParameterCountException(2, paramValues.Count());
            }
            string propertyName = paramValues.ElementAt(0) as string;
            string referenceName = paramValues.ElementAt(1) as string;
            AutomationPropertyIdentifier property = Properties.GetPropertyByName(propertyName);
            return new GetPropertyValue(context, property, referenceName, referenceValueStore);
        }
    }
}
