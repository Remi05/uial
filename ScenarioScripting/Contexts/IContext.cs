using System;
using System.Collections.Generic;
using System.Windows.Automation;
using ScenarioScripting.Interactions;

namespace ScenarioScripting.Contexts
{
    public interface IContext
    {
        string Name { get; }

        AutomationElement RootElement { get; }

        Dictionary<string, IContext> ChildrenContexts { get; }

        Dictionary<string, IInteraction> Interactions { get; }

        bool IsAvailable();
    }
}
