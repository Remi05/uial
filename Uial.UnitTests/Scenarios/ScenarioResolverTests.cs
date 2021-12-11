using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.DataModels;
using Uial.Interactions;
using Uial.Scenarios;
using Uial.UnitTests.Interactions;

namespace Uial.UnitTests.Scenarios
{
    [TestClass]
    public class ScenarioResolverTests
    {
        [TestMethod]
        public void VerifyNameIsSet()
        {
            string expectedName = "TestScenario";
            var scenarioDefinition = new ScenarioDefinition(expectedName, new List<BaseInteractionDefinition>());
            Assert.AreEqual(expectedName, scenarioDefinition.ScenarioName);
        }

        [TestMethod]
        public void VerifyResolvedScenarioHasSameName()
        {
            string expectedName = "TestScenario";
            var scenarioDefinition = new ScenarioDefinition(expectedName, new List<BaseInteractionDefinition>());
            var scenarioResolver = new ScenarioResolver();
            Scenario scenario = scenarioResolver.Resolve(scenarioDefinition, null, null);
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
            IEnumerable<BaseInteractionDefinition> mockBaseInteractions = mockInteractions.Select(
                (interaction) => new BaseInteractionDefinition(interaction.Name)
            );

            var scenarioDefinition = new ScenarioDefinition("TestScenario", mockBaseInteractions);
            var scenarioResolver = new ScenarioResolver();
            Scenario scenario = scenarioResolver.Resolve(scenarioDefinition, null, null);
            scenario.Do();

            Assert.IsTrue(interactionsToCall.SequenceEqual(interactionsCalled));
        }
    }
}
