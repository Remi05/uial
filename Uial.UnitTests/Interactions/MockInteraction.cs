using System;
using Uial.Interactions;

namespace Uial.UnitTests.Interactions
{
    class MockInteraction : IInteraction
    {
        public string Name { get; protected set; }
        protected Action DoAction { get; set; }
        protected int RunCount { get; set; } = 0;

        public bool WasRun => RunCount > 0;
        public bool WasRunOnce => RunCount == 1;

        public MockInteraction(string name = null, Action doAction = null)
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
