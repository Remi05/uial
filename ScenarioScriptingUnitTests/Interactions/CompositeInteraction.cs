using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScenarioScripting.Interactions;
using ScenarioScriptingUnitTests.Interactions;

namespace ScenarioScriptingUnitTests.Conditions
{
    [TestClass]
    public class CompositeInteractionTests
    {
        [TestMethod]
        public void VerifyNameIsSet()
        {
            string expectedName = "TestCompositeInteraction";
            var scenario = new CompositeInteraction(expectedName, new List<IInteraction>());
            Assert.AreEqual(expectedName, scenario.Name);
        }

        [TestMethod]
        public void VerifyAllInteractionsAreRunOnceInCorrectOrder()
        {
            var interactionsToCall = new List<string>() { "MockInteraction1", "MockInteraction2", "MockInteraction3", };
            var interactionsCalled = new List<string>();

            List<MockInteraction> mockInteractions = interactionsToCall.Select(
                (interactionName) => new MockInteraction(interactionName, () => interactionsCalled.Add(interactionName))
            ).ToList();

            var scenario = new CompositeInteraction("TestCompositeInteraction", mockInteractions);
            scenario.Do();

            Assert.IsTrue(mockInteractions.All((interaction) => interaction.WasRun), "All the interactions should be run.");
            Assert.IsTrue(mockInteractions.All((interaction) => interaction.WasRunOnce), "All the interactions should be run exactly once.");
            Assert.IsTrue(interactionsToCall.SequenceEqual(interactionsCalled), "All the interactions should be run in the order they were given.");
        }
    }
}
