using System;
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
            bool actualIsKnownInteraction = interactionProvider.IsInteractionAvailableForContext(interactionName, null);
            
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
            bool actualIsKnownInteraction = interactionProvider.IsInteractionAvailableForContext(interactionName, null);

            // Assert
            Assert.IsFalse(actualIsKnownInteraction);
        }

        [DataRow(IsAvailable.Key,        typeof(IsAvailable))]
        [DataRow(Wait.Key,               typeof(Wait))]
        [DataRow(WaitUntilAvailable.Key, typeof(WaitUntilAvailable))]
        [DataTestMethod]
        public void VerifyGettingKnownInteractionsReturnCorrectType(string interactionName, Type interactionType)
        {
            // Arrange
            var interactionProvider = new CoreInteractionProvider();

            // Act
            var interaction = interactionProvider.GetInteractionByName(interactionName, null, null);

            // Assert
            Assert.IsInstanceOfType(interaction, interactionType);
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
