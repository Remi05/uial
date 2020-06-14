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
        protected int DoCalledCount { get; set; } = 0;
        public bool WasCalledOnce => DoCalledCount == 1;

        public MockInteraction(string name)
        {
            Name = name;
        }

        public void Do()
        {
            ++DoCalledCount;
        }
    }
}
