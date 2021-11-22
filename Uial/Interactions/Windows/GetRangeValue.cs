using System;
using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Contexts.Windows;
using Uial.Scopes;

namespace Uial.Interactions.Windows
{
    public class GetRangeValue : AbstractPatternInteraction<IUIAutomationRangeValuePattern>, IInteraction
    {
        public const string Key = "GetRangeValue";

        public override string Name => Key;
        protected override int AutomationPattern => UIA_PatternIds.UIA_RangeValuePatternId;
        
        protected string ReferenceName { get; set; }
        protected RuntimeScope Scope { get; set; }

        public GetRangeValue(IWindowsVisualContext context, string referenceName, RuntimeScope scope) : base(context)
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
            Scope.ReferenceValues[ReferenceName] = Pattern.CurrentValue.ToString();
        }

        public static GetRangeValue FromRuntimeValues(IWindowsVisualContext context, RuntimeScope scope, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            string referenceName = paramValues.ElementAt(0);
            return new GetRangeValue(context, referenceName, scope);
        }
    }
}
