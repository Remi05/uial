using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Uial.DataModels.UnitTests
{
    [TestClass]
    public class LiteralValueDefinitionTests
    {
        [DataRow(null, DisplayName = "ValiLiteralValue_Null")]
        [DataRow("", DisplayName = "ValiLiteralValue_EmptyString")]
        [DataRow("SampleValue", DisplayName = "ValiLiteralValue_SampleString")]
        [DataRow(42, DisplayName = "ValiLiteralValue_SampleInt")]
        [DataRow(99.0, DisplayName = "ValiLiteralValue_SampleDouble")]
        [DataRow(99.0f, DisplayName = "ValiLiteralValue_SampleFloat")]
        [DataTestMethod]
        public void Constructor_ValidLiteralValueIsSet(object expectedLiteralValue)
        {
            // Act
            var literalValueDefinition = new LiteralValueDefinition(expectedLiteralValue);

            // Assert
            Assert.AreEqual(expectedLiteralValue, literalValueDefinition.LiteralValue);
        }
    }
}
