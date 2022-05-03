using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Uial.DataModels.UnitTests
{
    [TestClass]
    public class PropertyConditionDefinitionTests
    {
        [DataRow("TestProperty", DisplayName = "ValidName_SampleName")]
        [DataTestMethod]
        public void Constructor_ValidPropertyNameIsSet(string expectedName)
        {
            // Act
            var propertyConditionDefinition = new PropertyConditionDefinition(expectedName, new LiteralValueDefinition("TestValue"));

            // Assert
            Assert.AreEqual(expectedName, propertyConditionDefinition.PropertyName);
        }

        [TestMethod]
        public void Constructor_NullPropertyNameThrows()
        {
            // Act + Assert
            Assert.ThrowsException<ArgumentNullException>(() => new PropertyConditionDefinition(null, new LiteralValueDefinition("TestValue")));
        }

        [DataRow("", DisplayName = "InvalidPropertyName_Empty")]
        [DataRow("  ", DisplayName = "InvalidPropertyName_Spaces")]
        [DataRow("\t\t", DisplayName = "InvalidPropertyName_Tabs")]
        [DataRow("\n\r", DisplayName = "InvalidPropertyName_NewLines")]
        [DataTestMethod]
        public void Constructor_InvalidPropertyNameThrows(string propertyName)
        {
            // Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new PropertyConditionDefinition(propertyName, new LiteralValueDefinition("TestValue")));
        }

        [TestMethod]
        public void Constructor_NullValueThrows()
        {
            // Act + Assert
            Assert.ThrowsException<ArgumentNullException>(() => new PropertyConditionDefinition("TestProperty", null));
        }

        [TestMethod]
        public void Constructor_ValidReferenceValueIsSet()
        {
            // Arrange
            var expectedValue = new ReferenceValueDefinition("SampleRefValue");

            // Act
            var propertyConditionDefinition = new PropertyConditionDefinition("TestProperty", expectedValue);

            // Assert
            Assert.AreEqual(expectedValue, propertyConditionDefinition.Value);
        }

        [DataRow(null, DisplayName = "ValiLiteralValue_Null")]
        [DataRow("", DisplayName = "ValiLiteralValue_EmptyString")]
        [DataRow("SampleValue", DisplayName = "ValiLiteralValue_SampleString")]
        [DataRow(42, DisplayName = "ValiLiteralValue_SampleInt")]
        [DataRow(99.0, DisplayName = "ValiLiteralValue_SampleDouble")]
        [DataRow(99.0f, DisplayName = "ValiLiteralValue_SampleFloat")]
        [DataTestMethod]
        public void Constructor_ValidLiteralValueIsSet(object literalValue)
        {
            // Arrange
            var expectedValue = new LiteralValueDefinition(literalValue);

            // Act
            var propertyConditionDefinition = new PropertyConditionDefinition("TestProperty", expectedValue);

            // Assert
            Assert.AreEqual(expectedValue, propertyConditionDefinition.Value);
        }
    }
}
