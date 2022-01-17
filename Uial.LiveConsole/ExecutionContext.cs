using System.Collections.Generic;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Interactions;
using Uial.Modules;
using Uial.Scopes;
using Uial.Windows.Contexts;

namespace Uial.LiveConsole
{
    public class ExecutionContext
    {
        public Script Script { get; protected set; }
        public RuntimeScope RootScope { get; protected set; }
        public IContext RootContext { get; protected set; }
        public GlobalInteractionProvider InteractionProvider { get; protected set; }

        public ExecutionContext()
        {
            Script = new Script();
            RootScope = new RuntimeScope(Script.RootScope, new Values.ReferenceValueStore());
            RootContext = new RootVisualContext(RootScope);
            InteractionProvider = new GlobalInteractionProvider();
            InteractionProvider.AddProvider(new Interactions.Core.CoreInteractionProvider());
            InteractionProvider.AddProvider(new Windows.Interactions.VisualInteractionProvider());
        }

        public void AddModule(ModuleDefinition moduleDefinition)
        {
            var moduleProvider = new ModuleProvider();
            Module module = moduleProvider.GetModule(moduleDefinition);
            InteractionProvider = new GlobalInteractionProvider();
            InteractionProvider.AddMultipleProviders(module.InteractionProviders);
        }
    }
}
