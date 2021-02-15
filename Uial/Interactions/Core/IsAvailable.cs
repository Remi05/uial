using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.Interactions.Core
{
    public class IsAvailable : IInteraction
    {
        public const string Key = "IsAvailable";

        public string Name => Key;

        protected IContext Context { get; set; }
        protected string ReferenceName { get; set; }
        protected RuntimeScope Scope { get; set; }

        public IsAvailable(IContext context, string referenceName, RuntimeScope scope)
        {
            if (referenceName == null || scope == null)
            {
                throw new ArgumentNullException(referenceName == null ? nameof(referenceName): nameof(scope));
            }
            Context = context;
            ReferenceName = referenceName;
            Scope = scope;
        }

        public void Do()
        {
            Scope.ReferenceValues[ReferenceName] = (Context?.IsAvailable() ?? false).ToString();
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
