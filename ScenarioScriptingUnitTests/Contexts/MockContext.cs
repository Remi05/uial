using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using ScenarioScripting.Contexts;
using ScenarioScripting.Scopes;

namespace ScenarioScriptingUnitTests.Contexts
{
    class MockContext : IContext
    {
        public RuntimeScope Scope { get; protected set; }
        public string Name { get; protected set; }

        public AutomationElement RootElement { get; protected set; }

        protected bool Available { get; set;}

        public MockContext(RuntimeScope scope, string name, AutomationElement rootElement, bool isAvailable)
        {
            Scope = scope;
            Name = name;
            RootElement = rootElement;
            Available = isAvailable;
        }

        public bool IsAvailable() => Available;
    }
}
