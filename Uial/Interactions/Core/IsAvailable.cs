using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions.Core
{
    public class IsAvailable : AbstractInteraction, IInteraction
    {
        public const string Key = "IsAvailable";

        public override string Name => Key;

        protected string ReferenceName { get; set; }
        protected RuntimeScope Scope { get; set; }

        public IsAvailable(IContext context, string referenceName, RuntimeScope scope) : base(context)
        {
            if (referenceName == null || scope == null)
            {
                throw new ArgumentNullException(referenceName == null ? "referenceName" : "scope");
            }
            ReferenceName = referenceName;
            Scope = scope;
        }

        public override void Do()
        {
            base.Do();
            Scope.ReferenceValues[ReferenceName] = Context.IsAvailable().ToString();
        }

        public static IsAvailable FromRuntimeValues(IContext context, RuntimeScope scope, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            string referenceName = paramValues.ElementAt(0);
            return new IsAvailable(context, referenceName, scope);
        }
    }
}
