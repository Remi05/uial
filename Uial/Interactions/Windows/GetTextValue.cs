﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using Uial.Contexts.Windows;
using Uial.Scopes;

namespace Uial.Interactions.Windows
{
    public class GetTextValue : AbstractPatternInteraction<ValuePattern>, IInteraction
    {
        public const string Key = "GetTextValue";

        public override string Name => Key;
        protected override AutomationPattern AutomationPattern => ValuePattern.Pattern;

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
            Scope.ReferenceValues[ReferenceName] = Pattern.Current.Value;
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
