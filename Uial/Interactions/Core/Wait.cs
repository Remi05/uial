using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Uial.Interactions.Core
{
    public class Wait : IInteraction
    {
        public const string Key = "Wait";

        public string Name => Key;
        private TimeSpan Duration { get; set; }

        public Wait(TimeSpan duration)
        {
            Duration = duration;
        }

        public void Do()
        {
            Thread.Sleep(Duration);
        }

        public static Wait FromRuntimeValues(IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != 1)
            {
                throw new InvalidParameterCountException(1, paramValues.Count());
            }
            double milliseconds = double.Parse(paramValues.ElementAt(0));
            TimeSpan duration = TimeSpan.FromMilliseconds(milliseconds);
            return new Wait(duration);
        }
    }
}
