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
        public void VerifyResolvedScenarioHasSameName()
        {
            string expectedName = "TestScenario";
            ScenarioDefinition scenarioDefinition = new ScenarioDefinition(expectedName, new List<IBaseInteractionDefinition>());
            Scenario scenario = scenarioDefinition.Resolve(null);
            Assert.AreEqual(expectedName, scenario.Name);
        }

        [TestMethod]
        public void VerifyResolvedScenarioInteractionDefinitionsAreResolved()
        {
            List<MockInteraction> mockInteractions = new List<MockInteraction>
            {    
                new MockInteraction("MockInteraction1"),
                new MockInteraction("MockInteraction2"),
                new MockInteraction("MockInteraction3"),
            };
            IEnumerable<IBaseInteractionDefinition> mockBaseInteractions = mockInteractions.Select(
                (interaction) => new MockBaseInteractionDefinition(interaction)
            );

            ScenarioDefinition scenarioDefinition = new ScenarioDefinition("TestScenario", mockBaseInteractions);
            Scenario scenario = scenarioDefinition.Resolve(null);
            scenario.Do();

            Assert.IsTrue(mockInteractions.All((interaction) => interaction.WasCalledOnce));
        }
    }
}
