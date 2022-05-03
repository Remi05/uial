using Uial.Contexts;
using Uial.Interactions;
using Uial.Windows.Contexts;

namespace Uial.Windows.Interactions
{
    public abstract class AbstractInteraction : IInteraction
    {
        public abstract string Name { get; }
        protected IWindowsVisualContext Context { get; set; }

        public AbstractInteraction(IWindowsVisualContext context)
        {
            Context = context;
        }

        public virtual void Do()
        {
            if (Context == null || !Context.IsAvailable())
            {
                throw new ContextUnavailableException(Context.Name);
            }
        }
    }
}
