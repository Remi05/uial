using System;
using ScenarioScripting.Interactions;

namespace ScenarioScripting.UnitTests.Interactions
{
    class MockInteraction : IInteraction
    {
        public string Name { get; protected set; }
        protected Action DoAction { get; set; }
        protected int RunCount { get; set; } = 0;

        public bool WasRun => RunCount > 0;
        public bool WasRunOnce => RunCount == 1;

        public MockInteraction(string name, Action doAction = null)
        {
            Name = name;
            DoAction = doAction;
        }

        public void Do()
        {
            DoAction?.Invoke();
            ++RunCount;
        }
    }
}
