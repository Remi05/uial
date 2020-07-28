using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using Uial.Conditions;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions.Windows
{
    public class GetPropertyValue : AbstractInteraction, IInteraction
    {
        public const string Key = "GetPropertyValue";

        public override string Name => Key;

        protected AutomationProperty Property { get; set; }
        protected string ReferenceName { get; set; }
        protected RuntimeScope Scope { get; set; }

        public GetPropertyValue(IWindowsVisualContext context, AutomationProperty property, string referenceName, RuntimeScope scope) : base(context)
        {
            if (property == null || referenceName == null || scope == null)
            {
                throw new ArgumentNullException(property == null ? "property" : referenceName == null ? "referenceName" : "scope");
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
            AutomationProperty property = Properties.GetPropertyByName(propertyName);
            return new GetPropertyValue(context, property, referenceName, scope);
        }
    }
}
