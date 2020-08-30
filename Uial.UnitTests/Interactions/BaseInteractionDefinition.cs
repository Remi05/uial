using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Interactions;
using Uial.Scopes;
using Uial.UnitTests.Contexts;

namespace Uial.UnitTests.Interactions
{
    [TestClass]
    public class BaseInteractionDefinitionTests
    {
        [TestMethod]
        public void VerifyInteractionResolvedFromProvider()
        {
            // Arrange
            var expectedInteraction = new MockInteraction();

            string interactionName = "TestBaseInteractionDefinition";
            var baseInteractionDefinition = new BaseInteractionDefinition(interactionName, new List<ValueDefinition>());
            var interactionProvider = new MockInteractionProvider(new Dictionary<string, IInteraction>() { { interactionName, expectedInteraction } });
            var runtimeScope = new RuntimeScope(new DefinitionScope(), new Dictionary<string, string>());
            var parentContext = new MockContext(runtimeScope);

            // Act
            IInteraction actualInteraction = baseInteractionDefinition.Resolve(parentContext, interactionProvider, null);

            // Assert
            Assert.AreEqual(expectedInteraction, actualInteraction);
        }

        [TestMethod]
        public void VerifyInteractionResolvedFromScope()
        {
            // Arrange
            var expectedInteraction = new MockInteraction();

            string interactionName = "TestBaseInteractionDefinition";
            var mockInteractionDefinition = new MockInteractionDefinition(interactionName, expectedInteraction);
            var definitionScope = new DefinitionScope();
            definitionScope.InteractionDefinitions.Add(interactionName, mockInteractionDefinition);
            var runtimeScope = new RuntimeScope(definitionScope, new Dictionary<string, string>());
            var parentContext = new MockContext(runtimeScope);
            var baseInteractionDefinition = new BaseInteractionDefinition(interactionName, new List<ValueDefinition>());

            // Act
            IInteraction actualInteraction = baseInteractionDefinition.Resolve(parentContext, null, null);

            // Assert
            Assert.AreEqual(expectedInteraction, actualInteraction);
        }
    }
}

