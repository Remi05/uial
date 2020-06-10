using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using ScenarioScripting.Interactions;

namespace ScenarioScripting.Contexts
{
    public class RootContext : IContext
    {
        public string Name => "Root";
        public Dictionary<string, IContext> ChildrenContexts { get; private set; } = new Dictionary<string, IContext>();
        public Dictionary<string, IInteraction> Interactions { get; private set; } = new Dictionary<string, IInteraction>();
        public AutomationElement RootElement => AutomationElement.RootElement; 

        public bool IsAvailable() => true;
    }
}
