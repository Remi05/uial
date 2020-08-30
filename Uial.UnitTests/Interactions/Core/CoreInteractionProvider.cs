using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Interactions;
using Uial.Interactions.Core;

namespace Uial.UnitTests.Interactions.Core
{
    [TestClass]
    public class CoreInteractionProviderTests
    {
        [DataRow(IsAvailable.Key)]
        [DataRow(Wait.Key)]
        [DataRow(WaitUntilAvailable.Key)]
        [DataTestMethod]
        public void VerifyInteractionIsKnown(string interactionName)
        {
            // Arrange
            var interactionProvider = new CoreInteractionProvider();

            // Act
            bool actualIsKnownInteraction = interactionProvider.IsKnownInteraction(interactionName);
            
            // Assert
            Assert.IsTrue(actualIsKnownInteraction);
        }

        [DataRow("TestUnknownInteraction")]
        [DataTestMethod]
        public void VerifyInteractionIsNotKnown(string interactionName)
        {
            // Arrange
            var interactionProvider = new CoreInteractionProvider();

            // Act
            bool actualIsKnownInteraction = interactionProvider.IsKnownInteraction(interactionName);

            // Assert
            Assert.IsFalse(actualIsKnownInteraction);
        }

        [DataRow("TestUnknownInteraction")]
        [DataTestMethod]
        public void VerifyGettingUnknownInteractionThrows(string interactionName)
        {
            // Arrange
            var interactionProvider = new CoreInteractionProvider();

            // Act + Assert
            Assert.ThrowsException<InteractionUnavailableException>(() => interactionProvider.GetInteractionByName(null, null, interactionName, null));
        }
    }
}
