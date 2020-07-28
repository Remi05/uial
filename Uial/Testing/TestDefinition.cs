using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Contexts;
using Uial.Interactions;

namespace Uial.Testing
{
    public class TestDefinition : ITestableDefinition
    {
        public string Name { get; protected set; }
        protected IEnumerable<IBaseInteractionDefinition> BaseInteractionDefinitions { get; set; }

        public TestDefinition(string name, IEnumerable<IBaseInteractionDefinition> baseInteractionDefinitions)
        {
            if (name == null || baseInteractionDefinitions == null)
            {
                throw new ArgumentNullException(name == null ? "name" : "baseInteractionDefinitions");
            }
            Name = name;
            BaseInteractionDefinitions = baseInteractionDefinitions;
        }

        public ITestable Resolve(IContext context, IInteractionsProvider interactionsProvider)
        {
            IEnumerable<IInteraction> interactions = BaseInteractionDefinitions.Select(
                (interactionDefinition) => interactionDefinition.Resolve(context, interactionsProvider, context?.Scope)
            );
            return new Test(Name, interactions);
        }
    }
}
