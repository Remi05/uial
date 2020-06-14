using ScenarioScripting.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScenarioScriptingUnitTests.Interactions
{
    class MockInteraction : IInteraction
    {
        public string Name { get; protected set; }
        protected Action DoAction { get; set; }
        protected int DoCalledCount { get; set; } = 0;

        public bool WasCalled => DoCalledCount > 0;
        public bool WasCalledOnce => DoCalledCount == 1;

        public MockInteraction(string name, Action doAction = null)
        {
            Name = name;
            DoAction = doAction;
        }

        public void Do()
        {
            if (DoAction != null)
            {
                DoAction();
            }
            ++DoCalledCount;
        }
    }
}
