using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.DataModels;
using Uial.Interactions;
using Uial.Scopes;
using Uial.Values;
using Uial.UnitTests.Contexts;

namespace Uial.UnitTests.Interactions
{
    [TestClass]
    public class InteractionResolverTests
    {
        [TestMethod]
        public void VerifyNameIsSet()
        {
            // Arrange
            string expectedName = "TestInteractionDefinition";

            // Act
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), expectedName, new List<string>(), new List<BaseInteractionDefinition>());
            
            // Assert
            Assert.AreEqual(expectedName, interactionDefinition.Name);
        }

        [TestMethod]
        public void VerifyResolvedInteractionHasSameName()
        {
            // Arrange
            string expectedName = "TestInteractionDefinition";

            var runtimeScope = new RuntimeScope(new DefinitionScope(), new ReferenceValueStore());
            var parentContext = new MockContext(runtimeScope);
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), expectedName, new List<string>(), new List<BaseInteractionDefinition>());
            var interactionResolver = new InteractionResolver(null);
            
            // Act
            IInteraction interaction = interactionResolver.Resolve(interactionDefinition, new List<object>(), parentContext);
            
            // Assert
            Assert.AreEqual(expectedName, interaction.Name);
        }

        [TestMethod]
        public void VerifyResolvingWithMissingParamValuesThrows()
        {
            // Arrange
            var paramNames = new List<string>() { "TestParamName1" };
            var paramValues = new List<object>();

            var runtimeScope = new RuntimeScope(new DefinitionScope(), new ReferenceValueStore());
            var parentContext = new MockContext(runtimeScope);
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "", paramNames, new List<BaseInteractionDefinition>());
            var interactionResolver = new InteractionResolver(null);

            // Act + Assert
            Assert.ThrowsException<InvalidParameterCountException>(() => interactionResolver.Resolve(interactionDefinition, paramValues, parentContext));
        }

        [TestMethod]
        public void VerifyResolvingWithExtraParamValuesThrows()
        {
            // Arrange
            var paramNames = new List<string>();
            var paramValues = new List<object>() { "TestParamValue1" };

            var runtimeScope = new RuntimeScope(new DefinitionScope(), new ReferenceValueStore());
            var parentContext = new MockContext(runtimeScope);
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "", paramNames, new List<BaseInteractionDefinition>());
            var interactionResolver = new InteractionResolver(null);

            // Act + Assert
            Assert.ThrowsException<InvalidParameterCountException>(() => interactionResolver.Resolve(interactionDefinition, paramValues, parentContext));
        }

        [TestMethod]
        public void VerifyResolvedCompositeIteractionBaseInteractionDefinitionsAreResolved()
        {
            // TODO: Add Interactions to context

            // Arrange
            var interactionsToCall = new List<string>() { "MockInteraction1", "MockInteraction2", "MockInteraction3", };
            var interactionsCalled = new List<string>();

            IEnumerable<IInteraction> mockInteractions = interactionsToCall.Select(
                (interactionName) => new MockInteraction(interactionName, () => interactionsCalled.Add(interactionName))
            );
            IEnumerable<BaseInteractionDefinition> mockBaseInteractions = interactionsToCall.Select(
                (interactionName) => new BaseInteractionDefinition(interactionName)
            );

            var runtimeScope = new RuntimeScope(new DefinitionScope(), new ReferenceValueStore());
            var parentContext = new MockContext(runtimeScope);
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "", new List<string>(), mockBaseInteractions);
            var interactionResolver = new InteractionResolver(null);

            // Act
            IInteraction interaction = interactionResolver.Resolve(interactionDefinition, new List<object>(), parentContext);
            interaction.Do();

            // Assert
            Assert.IsTrue(interactionsToCall.SequenceEqual(interactionsCalled));
        }
    }
}
