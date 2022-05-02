using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Uial.Interactions;

namespace Uial.UnitTests.Interactions
{
    [TestClass]
    public class GlobalInteractionProviderTests
    {
        [TestMethod]
        public void VerifyInteractionIsKnown_SingleProvider()
        {
            // Arrange
            string interactionName = "TestKnownInteraction";
            var mockInteractionProvider = new MockInteractionProvider();
            mockInteractionProvider.InteractionsMap[interactionName] = new MockInteraction();

            var globalInteractionProvider = new GlobalInteractionProvider();
            globalInteractionProvider.AddProvider(mockInteractionProvider);

            // Act
            bool actualIsKnownInteraction = globalInteractionProvider.IsInteractionAvailableForContext(interactionName, null);

            // Assert
            Assert.IsTrue(actualIsKnownInteraction);
        }

        [TestMethod]
        public void VerifyInteractionIsKnown_MultipleProviders()
        {
            // Arrange
            string interactionName1 = "TestKnownInteraction1";
            string interactionName2 = "TestKnownInteraction2"; 

            var mockInteractionProvider1 = new MockInteractionProvider();
            mockInteractionProvider1.InteractionsMap[interactionName1] = new MockInteraction();
            var mockInteractionProvider2 = new MockInteractionProvider();
            mockInteractionProvider2.InteractionsMap[interactionName2] = new MockInteraction();

            var mockInteractionProviders = new List<IInteractionProvider>() { mockInteractionProvider1, mockInteractionProvider2 };
            var globalInteractionProvider = new GlobalInteractionProvider();
            globalInteractionProvider.AddMultipleProviders(mockInteractionProviders);

            // Act
            bool isFirstInteractionKnown = globalInteractionProvider.IsInteractionAvailableForContext(interactionName1, null);
            bool isSecondInteractionKnown = globalInteractionProvider.IsInteractionAvailableForContext(interactionName2, null);

            // Assert
            Assert.IsTrue(isFirstInteractionKnown);
            Assert.IsTrue(isSecondInteractionKnown);
        }

        [TestMethod]
        public void VerifyInteractionIsNotKnown_NoProviders()
        {
            // Arrange
            var globalInteractionProvider = new GlobalInteractionProvider();

            // Act
            bool actualIsKnownInteraction = globalInteractionProvider.IsInteractionAvailableForContext("TestUnknownInteraction", null);

            // Assert
            Assert.IsFalse(actualIsKnownInteraction);
        }

        [TestMethod]
        public void VerifyInteractionIsNotKnown_SingleProvider()
        {
            // Arrange
            var mockInteractionProvider = new MockInteractionProvider();
            var globalInteractionProvider = new GlobalInteractionProvider();
            globalInteractionProvider.AddProvider(mockInteractionProvider);

            // Act
            bool actualIsKnownInteraction = globalInteractionProvider.IsInteractionAvailableForContext("TestUnknownInteraction", null);

            // Assert
            Assert.IsFalse(actualIsKnownInteraction);
        }

        [TestMethod]
        public void VerifyGettingKnownInteractionReturnsInteraction()
        {
            // Arrange
            string interactionName1 = "TestKnownInteraction1";
            string interactionName2 = "TestKnownInteraction2";

            var expectedInteraction1 = new MockInteraction(interactionName1);
            var mockInteractionProvider1 = new MockInteractionProvider();
            mockInteractionProvider1.InteractionsMap[interactionName1] = expectedInteraction1;

            var expectedInteraction2 = new MockInteraction(interactionName2);
            var mockInteractionProvider2 = new MockInteractionProvider();
            mockInteractionProvider2.InteractionsMap[interactionName2] = expectedInteraction2;

            var mockInteractionProviders = new List<IInteractionProvider>() { mockInteractionProvider1, mockInteractionProvider2 };
            var globalInteractionProvider = new GlobalInteractionProvider();
            globalInteractionProvider.AddMultipleProviders(mockInteractionProviders);

            // Act
            var actualInteraction1 = globalInteractionProvider.GetInteractionByName(interactionName1, null, null);
            var actualInteraction2 = globalInteractionProvider.GetInteractionByName(interactionName2, null, null);

            // Assert
            Assert.AreEqual(expectedInteraction1, actualInteraction1);
            Assert.AreEqual(expectedInteraction2, actualInteraction2);
        }

        [TestMethod]
        public void VerifyGettingUnknownInteractionThrows()
        {
            // Arrange
            var mockInteractionProvider = new MockInteractionProvider();
            var globalInteractionProvider = new GlobalInteractionProvider();
            globalInteractionProvider.AddProvider(mockInteractionProvider);

            // Act + Assert
            Assert.ThrowsException<InteractionUnavailableException>(() => globalInteractionProvider.GetInteractionByName("TestUnknownInteraction", null, null));
        }

        [TestMethod]
        public void VerifyGettingInteractionKnownByMultipleProvidersThrows()
        {
            // Arrange
            string interactionName = "TestKnownInteraction";

            var mockInteractionProvider1 = new MockInteractionProvider();
            mockInteractionProvider1.InteractionsMap[interactionName] = new MockInteraction();
            var mockInteractionProvider2 = new MockInteractionProvider();
            mockInteractionProvider2.InteractionsMap[interactionName] = new MockInteraction();

            var mockInteractionProviders = new List<IInteractionProvider>() { mockInteractionProvider1, mockInteractionProvider2 };
            var globalInteractionProvider = new GlobalInteractionProvider();
            globalInteractionProvider.AddMultipleProviders(mockInteractionProviders);

            // Act + Assert
            Assert.ThrowsException<InteractionProviderConflictException>(() => globalInteractionProvider.GetInteractionByName(interactionName, null, null));
        }
    }
}
