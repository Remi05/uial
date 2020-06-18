using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Uial.Contexts;

namespace Uial.Interactions.Core
{
    public class WaitUntilAvailable : AbstractInteraction, IInteraction
    {
        public const string Key = "WaitUntilAvailable";

        public override string Name => Key;
        protected TimeSpan? Timeout { get; set; }

        public WaitUntilAvailable(IContext context, TimeSpan? timeout = null) : base(context) 
        {
            Timeout = timeout;
        }

        public override void Do()
        {
            base.Do();
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

        public static WaitUntilAvailable FromRuntimeValues(IContext context, IEnumerable<string> paramValues)
        {
            TimeSpan? timeout = null;
            if (paramValues.Count() > 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            else if (paramValues.Count() == 1)
            {
                double milliseconds = double.Parse(paramValues.ElementAt(0));
                timeout = TimeSpan.FromMilliseconds(milliseconds);
            }
            return new WaitUntilAvailable(context, timeout);
        }
    }
}
