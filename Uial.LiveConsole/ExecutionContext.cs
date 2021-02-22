using System.Collections.Generic;
using Uial.Contexts;
using Uial.Contexts.Windows;
using Uial.Interactions;
using Uial.Modules;
using Uial.Scopes;

namespace Uial.LiveConsole
{
    public class ExecutionContext
    {
        private List<IInteractionProvider> InteractionProviders { get; set; }
        public Script Script { get; protected set; }
        public RuntimeScope RootScope { get; protected set; }
        public IContext RootContext { get; protected set; }
        public IInteractionProvider InteractionProvider { get; protected set; }

        public ExecutionContext()
        {
            Script = new Script();
            RootScope = new RuntimeScope(Script.RootScope, new Dictionary<string, string>());
            RootContext = new RootVisualContext(RootScope);
            InteractionProviders = new List<IInteractionProvider>()
            {
                new Interactions.Core.CoreInteractionProvider(),
                new Interactions.Windows.VisualInteractionProvider(),
            };
            InteractionProvider = new GlobalInteractionProvider(InteractionProviders);
        }

        public void AddModule(ModuleDefinition moduleDefinition)
        {
            var moduleProvider = new ModuleProvider();
            Module module = moduleProvider.GetModule(moduleDefinition);
            InteractionProviders.AddRange(module.InteractionProviders);
            InteractionProvider = new GlobalInteractionProvider(InteractionProviders);
        }
    }
}
