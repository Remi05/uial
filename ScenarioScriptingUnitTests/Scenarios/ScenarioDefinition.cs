using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScenarioScripting.Interactions;
using ScenarioScripting.Scenarios;
using ScenarioScriptingUnitTests.Interactions;

namespace ScenarioScriptingUnitTests
{
    [TestClass]
    public class ScenarioDefinitionTests
    {
        [TestMethod]
        public void VerifyNameIsSet()
        {
            string expectedName = "TestScenario";
            ScenarioDefinition scenarioDefinition = new ScenarioDefinition(expectedName, new List<IBaseInteractionDefinition>());
            Assert.AreEqual(expectedName, scenarioDefinition.Name);
        }

        [TestMethod]
        public void ResolvedScenarioHasSameName()
        {
            string expectedName = "TestScenario";
            ScenarioDefinition scenarioDefinition = new ScenarioDefinition(expectedName, new List<IBaseInteractionDefinition>());
            Scenario scenario = scenarioDefinition.Resolve(null);
            Assert.AreEqual(expectedName, scenario.Name);
        }

        [TestMethod]
        public void ResolvedScenarioInteractionDefinitionsAreResolved()
        {
            List<string> interactionsToCall = new List<string>() { "MockInteraction1", "MockInteraction2", "MockInteraction3", };
            List<string> interactionsCalled = new List<string>();

            IEnumerable<IInteraction> mockInteractions = interactionsToCall.Select(
                (interactionName) => new MockInteraction(interactionName, () => interactionsCalled.Add(interactionName))
            );
            IEnumerable<IBaseInteractionDefinition> mockBaseInteractions = mockInteractions.Select(
                (interaction) => new MockBaseInteractionDefinition(interaction)
            );

            ScenarioDefinition scenarioDefinition = new ScenarioDefinition("TestScenario", mockBaseInteractions);
            Scenario scenario = scenarioDefinition.Resolve(null);
            scenario.Do();

            Assert.IsTrue(interactionsToCall.SequenceEqual(interactionsCalled));
        }
    }
}
