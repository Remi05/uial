using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Interactions;
using Uial.Scenarios;
using Uial.UnitTests.Interactions;

namespace Uial.UnitTests.Scenarios
{
    [TestClass]
    public class ScenarioTests
    {
        [TestMethod]
        public void VerifyNameIsSet()
        {
            string expectedName = "TestScenario";
            var scenario= new Scenario(expectedName, new List<IInteraction>());
            Assert.AreEqual(expectedName, scenario.Name);
        }

        [TestMethod]
        public void VerifyAllInteractionsAreRunOnceInCorrectOrder()
        {
            var interactionsToCall = new List<string>() { "MockInteraction1", "MockInteraction2", "MockInteraction3", };
            var interactionsCalled = new List<string>();

            var mockInteractions = new List<MockInteraction>();
            foreach (string interactionName in interactionsToCall)
            {
                var mockInteraction = new MockInteraction(interactionName, () => interactionsCalled.Add(interactionName));
                mockInteractions.Add(mockInteraction);
            }

            var scenario = new Scenario("TestScenario", mockInteractions);
            scenario.Do();

            Assert.IsTrue(mockInteractions.All((interaction) => interaction.WasRun), "All the interactions should be run.");
            Assert.IsTrue(mockInteractions.All((interaction) => interaction.WasRunOnce), "All the interactions should be run exactly once.");
            Assert.IsTrue(interactionsToCall.SequenceEqual(interactionsCalled), "All the interactions should be run in the order they were given.");
        }
    }
}
