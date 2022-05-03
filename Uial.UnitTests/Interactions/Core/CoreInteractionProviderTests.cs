using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.DataModels;
using Uial.Interactions;
using Uial.Interactions.Core;
using Uial.Scopes;
using Uial.Values;
using Uial.UnitTests.Contexts;

namespace Uial.UnitTests.Interactions.Core
{
    [TestClass]
    public class CoreInteractionProviderTests
    {
        [DataRow(IsAvailable.Key)]
        [DataRow(Wait.Key)]
        [DataRow(WaitUntilAvailable.Key)]
        [DataTestMethod]
        public void VerifyInteractionIsAvailable(string interactionName)
        {
            // Arrange
            var interactionProvider = new CoreInteractionProvider();

            // Act
            bool actualIsKnownInteraction = interactionProvider.IsInteractionAvailableForContext(interactionName, null);
            
            // Assert
            Assert.IsTrue(actualIsKnownInteraction);
        }

        [DataRow("TestUnknownInteraction")]
        [DataTestMethod]
        public void VerifyInteractionIsNotAvailable(string interactionName)
        {
            // Arrange
            var interactionProvider = new CoreInteractionProvider();

            // Act
            bool actualIsKnownInteraction = interactionProvider.IsInteractionAvailableForContext(interactionName, null);

            // Assert
            Assert.IsFalse(actualIsKnownInteraction);
        }

        [TestMethod]
        public void VerifyGetInteractionNameReturnsCorrectType_IsAvailable()
        {
            // Arrange
            var runtimeScope = new RuntimeScope(new DefinitionScope(), new ReferenceValueStore());
            var mockContext = new MockContext(runtimeScope);
            var interactionProvider = new CoreInteractionProvider();

            // Act
            var interaction = interactionProvider.GetInteractionByName(IsAvailable.Key, new object[]{ "$isAvailableValue" }, mockContext);

            // Assert
            Assert.IsInstanceOfType(interaction, typeof(IsAvailable));
        }

        [TestMethod]
        public void VerifyGetInteractionNameReturnsCorrectType_Wait()
        {
            // Arrange
            var interactionProvider = new CoreInteractionProvider();

            // Act
            var interaction = interactionProvider.GetInteractionByName(Wait.Key, new object[] { "10.0" }, null);

            // Assert
            Assert.IsInstanceOfType(interaction, typeof(Wait));
        }

        [TestMethod]
        public void VerifyGetInteractionNameReturnsCorrectType_WaitUntilAvailable()
        {
            // Arrange
            var interactionProvider = new CoreInteractionProvider();

            // Act
            var interaction = interactionProvider.GetInteractionByName(WaitUntilAvailable.Key, null, new MockContext());

            // Assert
            Assert.IsInstanceOfType(interaction, typeof(WaitUntilAvailable));
        }

        [DataRow("TestUnknownInteraction")]
        [DataTestMethod]
        public void VerifyGettingUnknownInteractionThrows(string interactionName)
        {
            // Arrange
            var interactionProvider = new CoreInteractionProvider();

            // Act + Assert
            Assert.ThrowsException<InteractionUnavailableException>(() => interactionProvider.GetInteractionByName(interactionName, null, null));
        }
    }
}
