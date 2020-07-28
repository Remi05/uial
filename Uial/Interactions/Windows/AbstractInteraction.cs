using Uial.Contexts;

namespace Uial.Interactions.Windows
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
