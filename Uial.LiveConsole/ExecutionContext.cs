using System.Collections.Generic;
using Uial.Contexts;
using Uial.Contexts.Windows;
using Uial.Interactions;
using Uial.Scopes;

namespace Uial.LiveConsole
{
    public class ExecutionContext
    {
        public Script Script { get; protected set; }
        public RuntimeScope RootScope { get; protected set; }
        public IContext RootContext { get; protected set; }
        public IInteractionProvider InteractionProvider { get; protected set; }

        public ExecutionContext()
        {
            Script = new Script();
            RootScope = new RuntimeScope(Script.RootScope, new Dictionary<string, string>());
            RootContext = new RootVisualContext(RootScope);
            var interactionProviders = new List<IInteractionProvider>()
            {
                new Interactions.Core.CoreInteractionProvider(),
                new Interactions.Windows.VisualInteractionProvider(),
            };
            InteractionProvider = new GlobalInteractionProvider(interactionProviders);
        }
    }
}
