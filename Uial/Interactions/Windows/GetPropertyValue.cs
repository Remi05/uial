using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Conditions;
using Uial.Contexts.Windows;
using Uial.Scopes;

using AutomationPropertyIdentifier = System.Int32;

namespace Uial.Interactions.Windows
{
    public class GetPropertyValue : AbstractInteraction, IInteraction
    {
        public const string Key = "GetPropertyValue";

        public override string Name => Key;

        protected AutomationPropertyIdentifier Property { get; set; }
        protected string ReferenceName { get; set; }
        protected RuntimeScope Scope { get; set; }

        public GetPropertyValue(IWindowsVisualContext context, AutomationPropertyIdentifier property, string referenceName, RuntimeScope scope) : base(context)
        {
            if (referenceName == null || scope == null)
            {
                throw new ArgumentNullException(referenceName == null ? nameof(referenceName) : nameof(scope));
            }
            Property = property;
            ReferenceName = referenceName;
            Scope = scope;
        }

        public override void Do()
        {
            base.Do();
            object propertyValue = Context.RootElement.GetCurrentPropertyValue(Property);
            Scope.ReferenceValues[ReferenceName] = propertyValue?.ToString();
        }

        public static GetPropertyValue FromRuntimeValues(IWindowsVisualContext context, RuntimeScope scope, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 2)
            {
                throw new InvalidParameterCountException(2, paramValues.Count());
            }
            string propertyName = paramValues.ElementAt(0);
            string referenceName = paramValues.ElementAt(1);
            AutomationPropertyIdentifier property = Properties.GetPropertyByName(propertyName);
            return new GetPropertyValue(context, property, referenceName, scope);
        }
    }
}
