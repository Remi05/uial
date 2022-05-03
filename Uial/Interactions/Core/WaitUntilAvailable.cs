using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Uial.Contexts;

namespace Uial.Interactions.Core
{
    public class WaitUntilAvailable : IInteraction
    {
        public const string Key = "WaitUntilAvailable";

        public string Name => Key;
        protected IContext Context { get; set; }
        protected TimeSpan? Timeout { get; set; }

        public WaitUntilAvailable(IContext context, TimeSpan? timeout = null)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            Context = context;
            Timeout = timeout;
        }

        public void Do()
        {
            DateTime start = DateTime.Now;
            while (!Context.IsAvailable())
            {
                if (Timeout != null && DateTime.Now - start >= Timeout)
                {
                    throw new ContextNotFoundInTimeException(Context.Name, (TimeSpan)Timeout);
                }
                Thread.Sleep(100);
            }
        }

        public static WaitUntilAvailable FromRuntimeValues(IContext context, IEnumerable<object> paramValues)
        {
            TimeSpan? timeout = null;
            if (paramValues == null)
            {
                throw new ArgumentNullException(nameof(paramValues));
            }
            if (paramValues.Count() > 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            if (paramValues.Count() == 1)
            {
                double milliseconds = double.Parse(paramValues.ElementAt(0) as string);
                timeout = TimeSpan.FromMilliseconds(milliseconds);
            }
            return new WaitUntilAvailable(context, timeout);
        }
    }
}
