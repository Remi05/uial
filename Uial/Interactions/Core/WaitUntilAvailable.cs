using System.Threading;
using Uial.Contexts;

namespace Uial.Interactions.Core
{
    public class WaitUntilAvailable : AbstractInteraction, IInteraction
    {
        public const string Key = "WaitUntilAvailable";

        public override string Name => Key;

        public WaitUntilAvailable(IContext context) : base(context) { }

        public override void Do()
        {
            base.Do();
            while (!Context.IsAvailable())
            {
                Thread.Sleep(100);
            }
        }
    }
}
