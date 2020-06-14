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

        public MockInteraction(string name, Action doAction)
        {
            Name = name;
            DoAction = doAction;
        }

        public void Do()
        {
            DoAction();
        }
    }
}
