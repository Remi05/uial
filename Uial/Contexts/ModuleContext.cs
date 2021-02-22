using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uial.Interactions;
using Uial.Modules;
using Uial.Scopes;

namespace Uial.Contexts
{
    public class ModuleContext : IContext
    {
        protected Module Module { get; set; }
        public string Name => Module.Name;
        public RuntimeScope Scope { get; protected set; }
        public IInteractionProvider InteractionProvider { get; protected set; }

        public ModuleContext(IContext parentContext, Module module)
        {
            Module = module;

            var interactionProviders = new List<IInteractionProvider>(module.InteractionProviders);
            interactionProviders.Add(parentContext.InteractionProvider);
            InteractionProvider = new GlobalInteractionProvider(interactionProviders);
        }

        public bool IsAvailable() => true;
    }
}
