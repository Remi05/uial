using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Interactions;
using Uial.Scopes;
using Uial.UnitTests.Contexts;

namespace Uial.UnitTests.Interactions
{
    [TestClass]
    public class InteractionDefinitionTests
    {
        [TestMethod]
        public void VerifyNameIsSet()
        {
            // Arrange
            string expectedName = "TestInteractionDefinition";

            // Act
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), expectedName, new List<string>(), new List<IBaseInteractionDefinition>());
            
            // Assert
            Assert.AreEqual(expectedName, interactionDefinition.Name);
        }

        [TestMethod]
        public void VerifyResolvedInteractionHasSameName()
        {
            // Arrange
            string expectedName = "TestInteractionDefinition";

            var runtimeScope = new RuntimeScope(new DefinitionScope(), new Dictionary<string, string>());
            var parentContext = new MockContext(runtimeScope);
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), expectedName, new List<string>(), new List<IBaseInteractionDefinition>());
            
            // Act
            IInteraction interaction = interactionDefinition.Resolve(parentContext, null, new List<string>());
            
            // Assert
            Assert.AreEqual(expectedName, interaction.Name);
        }

        [TestMethod]
        public void VerifyResolvingWithMissingParamValuesThrows()
        {
            // Arrange
            var paramNames = new List<string>() { "TestParamName1" };
            var paramValues = new List<string>();

            var runtimeScope = new RuntimeScope(new DefinitionScope(), new Dictionary<string, string>());
            var parentContext = new MockContext(runtimeScope);
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "", paramNames, new List<IBaseInteractionDefinition>());

            // Act + Assert
            Assert.ThrowsException<InvalidParameterCountException>(() => interactionDefinition.Resolve(parentContext, null, paramValues));
        }

        [TestMethod]
        public void VerifyResolvingWithExtraParamValuesThrows()
        {
            // Arrange
            var paramNames = new List<string>();
            var paramValues = new List<string>() { "TestParamValue1" };

            var runtimeScope = new RuntimeScope(new DefinitionScope(), new Dictionary<string, string>());
            var parentContext = new MockContext(runtimeScope);
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "", paramNames, new List<IBaseInteractionDefinition>());

            // Act + Assert
            Assert.ThrowsException<InvalidParameterCountException>(() => interactionDefinition.Resolve(parentContext, null, paramValues));
        }

        [TestMethod]
        public void VerifyResolvedCompositeIteractionBaseInteractionDefinitionsAreResolved()
        {
            // Arrange
            var interactionsToCall = new List<string>() { "MockInteraction1", "MockInteraction2", "MockInteraction3", };
            var interactionsCalled = new List<string>();

            IEnumerable<IInteraction> mockInteractions = interactionsToCall.Select(
                (interactionName) => new MockInteraction(interactionName, () => interactionsCalled.Add(interactionName))
            );
            IEnumerable<IBaseInteractionDefinition> mockBaseInteractions = mockInteractions.Select(
                (mockInteraction) => new MockBaseInteractionDefinition(mockInteraction)
            );

            var runtimeScope = new RuntimeScope(new DefinitionScope(), new Dictionary<string, string>());
            var parentContext = new MockContext(runtimeScope);
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "", new List<string>(), mockBaseInteractions);

            // Act
            IInteraction interaction = interactionDefinition.Resolve(parentContext, null, new List<string>());
            interaction.Do();

            // Assert
            Assert.IsTrue(interactionsToCall.SequenceEqual(interactionsCalled));
        }
    }
}
