using System;
using System.Collections.Generic;
using System.Windows.Automation;
using ScenarioScripting.Interactions;

namespace ScenarioScripting.Contexts
{
    public abstract class AbstractContext : IContext
    {
        public string Name { get; protected set; }
        public Condition UniqueCondition { get; protected set; }
        public Dictionary<string, IContext> ChildrenContexts { get; protected set; } = new Dictionary<string, IContext>();
        public Dictionary<string, IInteraction> Interactions { get; protected set; } = new Dictionary<string, IInteraction>();

        public abstract AutomationElement RootElement { get; }

        public bool IsAvailable()
        {
            return RootElement != null
                && (UniqueCondition == null
                || RootElement.FindFirst(TreeScope.Subtree, UniqueCondition) != null);
        }
    }
}
