using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Interactions;
using Uial.Scenarios;
using Uial.UnitTests.Interactions;

namespace Uial.UnitTests
{
    [TestClass]
    public class ScenarioDefinitionTests
    {
        [TestMethod]
        public void VerifyNameIsSet()
        {
            string expectedName = "TestScenario";
            var scenarioDefinition = new ScenarioDefinition(expectedName, new List<IBaseInteractionDefinition>());
            Assert.AreEqual(expectedName, scenarioDefinition.Name);
        }

        [TestMethod]
        public void VerifyResolvedScenarioHasSameName()
        {
            string expectedName = "TestScenario";
            var scenarioDefinition = new ScenarioDefinition(expectedName, new List<IBaseInteractionDefinition>());
            Scenario scenario = scenarioDefinition.Resolve(null);
            Assert.AreEqual(expectedName, scenario.Name);
        }

        [TestMethod]
        public void VerifyResolvedScenarioInteractionDefinitionsAreResolved()
        {
            var interactionsToCall = new List<string>() { "MockInteraction1", "MockInteraction2", "MockInteraction3", };
            var interactionsCalled = new List<string>();

            IEnumerable<IInteraction> mockInteractions = interactionsToCall.Select(
                (interactionName) => new MockInteraction(interactionName, () => interactionsCalled.Add(interactionName))
            );
            IEnumerable<IBaseInteractionDefinition> mockBaseInteractions = mockInteractions.Select(
                (interaction) => new MockBaseInteractionDefinition(interaction)
            );

            var scenarioDefinition = new ScenarioDefinition("TestScenario", mockBaseInteractions);
            Scenario scenario = scenarioDefinition.Resolve(null);
            scenario.Do();

            Assert.IsTrue(interactionsToCall.SequenceEqual(interactionsCalled));
        }
    }
}
