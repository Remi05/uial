using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.DataModels;
using Uial.Interactions;
using Uial.Scenarios;
using Uial.UnitTests.Interactions;

namespace Uial.UnitTests.Contexts
{
    [TestClass]
    public class ContextResolverTests
    {
        [TestMethod]
        public void VerifyResolvedScenarioInteractionDefinitionsAreResolved()
        {
            //var interactionsToCall = new List<string>() { "MockInteraction1", "MockInteraction2", "MockInteraction3", };
            //var interactionsCalled = new List<string>();

            //IEnumerable<IInteraction> mockInteractions = interactionsToCall.Select(
            //    (interactionName) => new MockInteraction(interactionName, () => interactionsCalled.Add(interactionName))
            //);
            //IEnumerable<BaseInteractionDefinition> mockBaseInteractions = mockInteractions.Select(
            //    (interaction) => new BaseInteractionDefinition(interaction.Name)
            //);

            //var scenarioDefinition = new ScenarioDefinition("TestScenario", mockBaseInteractions);
            //var scenarioResolver = new ScenarioResolver();
            //Scenario scenario = scenarioResolver.Resolve(scenarioDefinition, null, null);
            //scenario.Do();

            //Assert.IsTrue(interactionsToCall.SequenceEqual(interactionsCalled));
        }
    }
}
