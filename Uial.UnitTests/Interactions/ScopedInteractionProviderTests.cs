using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Uial.Interactions;

namespace Uial.UnitTests.Interactions.Core
{
    [TestClass]
    public class ScopedInteractionProviderTests
    {
        [TestMethod]
        public void VerifyGettingUnknownInteractionThrows()
        {
            //// Arrange
            //var mockInteractionProvider = new MockInteractionProvider();
            //var globalInteractionProvider = new ScopedInteractionProvider();
            //globalInteractionProvider.AddProvider(mockInteractionProvider);

            //// Act + Assert
            //Assert.ThrowsException<InteractionUnavailableException>(() => globalInteractionProvider.GetInteractionByName("TestUnknownInteraction", null, null));
        }
    }
}
