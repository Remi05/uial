using System.Collections.Generic;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Interactions
{
    public class CompositeInteraction : IInteraction
    {
        public string Name { get; protected set; }

        protected List<IInteraction> Interactions { get; set; }

        public CompositeInteraction(string name, List<IInteraction> interactions)
        {
            // throw if either param is null
            Name = name;
            Interactions = interactions;
        }

        public void Do(IContext context)
        {
            foreach (IInteraction interaction in Interactions)
            {
                interaction.Do(context);
            }
        }

        public bool IsAvailable(IContext context)
        {
            return true;
        }
    }
}
