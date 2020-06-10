using System.Collections.Generic;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public class CompositeInteraction : IInteraction
    {
        public string Name { get; protected set; }

        protected List<IInteraction> Interactions { get; set; }

        protected IContext Context { get; set; }

        public CompositeInteraction(IContext context, string name, List<IInteraction> interactions)
        {
            // throw if any param is null
            Context = context;
            Name = name;
            Interactions = interactions;
        }

        public void Do()
        {
            foreach (IInteraction interaction in Interactions)
            {
                interaction.Do();
            }
        }
    }
}
