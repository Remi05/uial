using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ScenarioScripting.Interactions.Core
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
            TimeSpan duration = TimeSpan.Parse(paramValues.ElementAt(0));
            return new Wait(duration);
        }
    }
}
