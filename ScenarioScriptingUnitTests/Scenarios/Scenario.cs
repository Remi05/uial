using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScenarioScripting.Interactions;
using ScenarioScripting.Scenarios;
using ScenarioScriptingUnitTests.Interactions;

namespace ScenarioScriptingUnitTests.Scenarios
{
    [TestClass]
    public class ScenarioTests
    {
        [TestMethod]
        public void VerifyNameIsSet()
        {
            string expectedName = "TestScenario";
            Scenario scenario= new Scenario(expectedName, new List<IInteraction>());
            Assert.AreEqual(expectedName, scenario.Name);
        }

        [TestMethod]
        public void VerifyAllInteractionsAreRun()
        {
            List<MockInteraction> mockInteractions = new List<MockInteraction>()
            {
                new MockInteraction("MockInteraction1"),
                new MockInteraction("MockInteraction2"),
                new MockInteraction("MockInteraction3"),
            };

            Scenario scenario = new Scenario("TestScenario", mockInteractions);
            scenario.Do();

            Assert.IsTrue(mockInteractions.All((interaction) => interaction.WasCalled));
        }

        [TestMethod]
        public void VerifyAllInteractionsAreRunOnce()
        {
            List<MockInteraction> mockInteractions = new List<MockInteraction>()
            {
                new MockInteraction("MockInteraction1"),
                new MockInteraction("MockInteraction2"),
                new MockInteraction("MockInteraction3"),
            };

            Scenario scenario = new Scenario("TestScenario", mockInteractions);
            scenario.Do();

            Assert.IsTrue(mockInteractions.All((interaction) => interaction.WasCalledOnce));
        }

        [TestMethod]
        public void VerifyAllInteractionsAreRunInCorrectOrder()
        {
            List<string> interactionsToCall = new List<string>() { "MockInteraction1", "MockInteraction2", "MockInteraction3", };
            List<string> interactionsCalled = new List<string>();

            IEnumerable<IInteraction> mockInteractions = interactionsToCall.Select(
                (interactionName) => new MockInteraction(interactionName, () => interactionsCalled.Add(interactionName))
            );

            Scenario scenario = new Scenario("TestScenario", mockInteractions);
            scenario.Do();

            Assert.IsTrue(interactionsToCall.SequenceEqual(interactionsCalled));
        }
    }
}
