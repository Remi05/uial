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
            // Arrange
            string expectedName = "TestScenario";

            // Act
            var scenarioDefinition = new ScenarioDefinition(expectedName, new List<BaseInteractionDefinition>());

            // Assert
            Assert.AreEqual(expectedName, scenarioDefinition.Name);
        }

        [TestMethod]
        public void VerifyResolvedScenarioHasSameName()
        {
            // Arrange
            string expectedName = "TestScenario";
            var scenarioDefinition = new ScenarioDefinition(expectedName, new List<BaseInteractionDefinition>());
            var scenarioResolver = new ScenarioResolver(null);
            
            // Act
            Scenario scenario = scenarioResolver.Resolve(scenarioDefinition, null, null);

            // Assert
            Assert.AreEqual(expectedName, scenario.Name);
        }

        [TestMethod]
        public void VerifyResolvedScenarioInteractionDefinitionsAreResolved()
        {
            // Arrange
            var interactionsToCall = new List<string>() { "MockInteraction1", "MockInteraction2", "MockInteraction3", };
            var interactionsCalled = new List<string>();

            var mockInteractions = new Dictionary<BaseInteractionDefinition, IInteraction>();
            foreach (var interactionName in interactionsToCall)
            {
                var mockInteraction = new MockInteraction(interactionName, () => interactionsCalled.Add(interactionName));
                var baseInteractionDefinition = new BaseInteractionDefinition(interactionName);
                mockInteractions[baseInteractionDefinition] = mockInteraction;
            }

            var mockBaseInteractionResolver = new MockBaseInteractionResolver(mockInteractions);
            var scenarioDefinition = new ScenarioDefinition("TestScenario", mockInteractions.Keys);
            var scenarioResolver = new ScenarioResolver(mockBaseInteractionResolver);

            // Act
            Scenario scenario = scenarioResolver.Resolve(scenarioDefinition, null, null);
            scenario.Do();

            // Assert
            Assert.IsTrue(interactionsToCall.SequenceEqual(interactionsCalled));
        }
    }
}
