using System;
using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Contexts.Windows;
using Uial.Scopes;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows
{
    public class GetTextValue : AbstractPatternInteraction<IUIAutomationValuePattern>, IInteraction
    {
        public const string Key = "GetTextValue";

        public override string Name => Key;
        protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_ValuePatternId;

        protected string ReferenceName { get; set; }
        protected RuntimeScope Scope { get; set; }

        public GetTextValue(IWindowsVisualContext context, string referenceName, RuntimeScope scope) : base(context)
        {
            if (referenceName == null || scope == null)
            {
                throw new ArgumentNullException(referenceName == null ? nameof(referenceName) : nameof(scope));
            }
            ReferenceName = referenceName;
            Scope = scope;
        }

        public override void Do()
        {
            base.Do();
            Scope.ReferenceValues[ReferenceName] = Pattern.CurrentValue;
        }

        public static GetTextValue FromRuntimeValues(IWindowsVisualContext context, RuntimeScope scope, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            string referenceName = paramValues.ElementAt(0);
            return new GetTextValue(context, referenceName, scope);
        }
    }
}
