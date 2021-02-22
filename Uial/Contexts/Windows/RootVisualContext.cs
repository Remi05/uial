﻿using System.Windows.Automation;
using Uial.Interactions;
using Uial.Scopes;

namespace Uial.Contexts.Windows
{
    public class RootVisualContext : IWindowsVisualContext
    {
        public RuntimeScope Scope { get; private set; }
        public string Name => "Root";
        public AutomationElement RootElement => AutomationElement.RootElement;
        public IInteractionProvider InteractionProvider { get; protected set; }

        public RootVisualContext(RuntimeScope scope)
        {
            Scope = scope;
        }

        public bool IsAvailable()
        {
            return RootElement != null;
        }
    }
}
