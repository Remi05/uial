using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Values;

namespace Uial.Interactions.Core
{
    public class IsAvailable : IInteraction
    {
        public const string Key = "IsAvailable";

        public string Name => Key;

        protected IContext Context { get; set; }
        protected string ReferenceName { get; set; }
        protected IReferenceValueStore ReferenceValueStore { get; set; }

        public IsAvailable(IContext context, string referenceName, IReferenceValueStore referenceValueStore)
        {
            if (referenceName == null || referenceValueStore == null)
            {
                throw new ArgumentNullException(referenceName == null ? nameof(referenceName): nameof(referenceValueStore));
            }
            Context = context;
            ReferenceName = referenceName;
            ReferenceValueStore = referenceValueStore;
        }

        public void Do()
        {
            ReferenceValueStore.SetValue(ReferenceName, (Context?.IsAvailable() ?? false).ToString());
        }

        public static IsAvailable FromRuntimeValues(IContext context, IEnumerable<object> paramValues, IReferenceValueStore referenceValueStore)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            string referenceName = paramValues.ElementAt(0) as string;
            return new IsAvailable(context, referenceName, referenceValueStore);
        }
    }
}
