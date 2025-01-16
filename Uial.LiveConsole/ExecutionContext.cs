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
        public IContext RootContext { get; protected set; }
        public IWindowsVisualContext RootVisualContext => RootContext as IWindowsVisualContext;
        public RuntimeScope RootScope => RootContext.Scope;
        public IInteractionProvider InteractionProvider { get; protected set; }
        protected Stack<IContext> ContextsStack { get; set; } = new();

        public ExecutionContext()
        {
            Script = new Script();
            RuntimeScope initialScope = new(Script.RootScope, new Dictionary<string, string>());
            RootContext = new RootVisualContext(initialScope);
            InteractionProviders = new List<IInteractionProvider>()
            {
                new Interactions.Core.CoreInteractionProvider(),
                new Interactions.Windows.VisualInteractionProvider(),
                new Snapshots.SnapshotsInteractionProvider(),
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

        public void PushContext(IContext context)
        {
            ContextsStack.Push(RootContext);
            RootContext = context;
        }

        public void PopContext()
        {
            IContext previousContext = ContextsStack.Pop(); 
            RootContext = previousContext;
        }
    }
}
