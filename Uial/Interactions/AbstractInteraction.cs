using Uial.Contexts;

namespace Uial.Interactions
{
    public abstract class AbstractInteraction : IInteraction
    {
        public abstract string Name { get; }
        protected IContext Context { get; set; }

        public AbstractInteraction(IContext context)
        {
            Context = context;
        }

        public virtual void Do()
        {
            if (!Context.IsAvailable())
            {
                throw new ContextUnavailableException(Context.Name);
            }
        }
    }
}
