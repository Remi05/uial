using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Uial.Interactions;

namespace Uial.UnitTests.Interactions.Core
{
    [TestClass]
    public class GlobalInteractionProviderTests
    {
        [TestMethod]
        public void VerifyInteractionIsKnown_SingleProvider()
        {
            // Arrange
            string interactionName = "TestKnownInteraction";
            var knownInteractions = new Dictionary<string, IInteraction>() { { interactionName, new MockInteraction() } };
            var mockInteractionProvider = new MockInteractionProvider(knownInteractions);
            var mockInteractionProviders = new List<IInteractionProvider>() { mockInteractionProvider };
            var globalInteractionProvider = new GlobalInteractionProvider(mockInteractionProviders);

            // Act
            bool actualIsKnownInteraction = globalInteractionProvider.IsKnownInteraction(interactionName);

            // Assert
            Assert.IsTrue(actualIsKnownInteraction);
        }

        [TestMethod]
        public void VerifyInteractionIsKnown_MultipleProviders()
        {
            // Arrange
            string interactionName1 = "TestKnownInteraction1";
            string interactionName2 = "TestKnownInteraction2"; 
            var knownInteractions1 = new Dictionary<string, IInteraction>() { { interactionName1, new MockInteraction() } };
            var knownInteractions2 = new Dictionary<string, IInteraction>() { { interactionName2, new MockInteraction() } }; 
            var mockInteractionProvider1 = new MockInteractionProvider(knownInteractions1);
            var mockInteractionProvider2 = new MockInteractionProvider(knownInteractions2);
            var mockInteractionProviders = new List<IInteractionProvider>() { mockInteractionProvider1, mockInteractionProvider2 };
            var globalInteractionProvider = new GlobalInteractionProvider(mockInteractionProviders);

            // Act
            bool isFirstInteractionKnown = globalInteractionProvider.IsKnownInteraction(interactionName1);
            bool isSecondInteractionKnown = globalInteractionProvider.IsKnownInteraction(interactionName2);

            // Assert
            Assert.IsTrue(isFirstInteractionKnown);
            Assert.IsTrue(isSecondInteractionKnown);
        }

        [TestMethod]
        public void VerifyInteractionIsNotKnown_NoProviders()
        {
            // Arrange
            var globalInteractionProvider = new GlobalInteractionProvider(new List<IInteractionProvider>());

            // Act
            bool actualIsKnownInteraction = globalInteractionProvider.IsKnownInteraction("TestUnknownInteraction");

            // Assert
            Assert.IsFalse(actualIsKnownInteraction);
        }

        [TestMethod]
        public void VerifyInteractionIsNotKnown_SingleProvider()
        {
            // Arrange
            var mockInteractionProvider = new MockInteractionProvider(new Dictionary<string, IInteraction>());
            var mockInteractionProviders = new List<IInteractionProvider>() { mockInteractionProvider };
            var globalInteractionProvider = new GlobalInteractionProvider(mockInteractionProviders);

            // Act
            bool actualIsKnownInteraction = globalInteractionProvider.IsKnownInteraction("TestUnknownInteraction");

            // Assert
            Assert.IsFalse(actualIsKnownInteraction);
        }


        [TestMethod]
        public void VerifyGettingUnknownInteractionThrows()
        {
            // Arrange
            var mockInteractionProvider = new MockInteractionProvider(new Dictionary<string, IInteraction>());
            var mockInteractionProviders = new List<IInteractionProvider>() { mockInteractionProvider };
            var globalInteractionProvider = new GlobalInteractionProvider(mockInteractionProviders);

            // Act + Assert
            Assert.ThrowsException<InteractionUnavailableException>(() => globalInteractionProvider.GetInteractionByName(null, null, "TestUnknownInteraction", null));
        }
    }
}
