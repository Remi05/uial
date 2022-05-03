using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Uial.DataModels.UnitTests
{
    [TestClass]
    public class ReferenceValueDefinitionTests
    {
        [DataRow("$testValue", DisplayName = "ValidReferenceName_SampleName")]
        [DataTestMethod]
        public void Constructor_ValidReferenceNameIsSet(string expectedReferenceName)
        {
            // Act
            var referenceValueDefinition = new ReferenceValueDefinition(expectedReferenceName);

            // Assert
            Assert.AreEqual(expectedReferenceName, referenceValueDefinition.ReferenceName);
        }

        [DataRow("", DisplayName = "InvalidPropertyName_Empty")]
        [DataRow("  ", DisplayName = "InvalidPropertyName_Spaces")]
        [DataRow("\t\t", DisplayName = "InvalidPropertyName_Tabs")]
        [DataRow("\n\r", DisplayName = "InvalidPropertyName_NewLines")]
        [DataTestMethod]
        public void Constructor_InvalidReferenceNameThrows(string referenceName)
        {
            // Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new ReferenceValueDefinition(referenceName));
        }

        [TestMethod]
        public void Constructor_NullReferenceNameThrows()
        {
            // Act + Assert
            Assert.ThrowsException<ArgumentNullException>(() => new ReferenceValueDefinition(null));
        }
    }
}
