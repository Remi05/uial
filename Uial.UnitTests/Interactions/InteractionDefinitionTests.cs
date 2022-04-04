using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.DataModels;

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
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), expectedName, new List<string>(), new List<BaseInteractionDefinition>());

            // Assert
            Assert.AreEqual(expectedName, interactionDefinition.Name);
        }
    }
}
