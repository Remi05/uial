using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Uial.DataModels.UnitTests
{
    [TestClass]
    public class CompositeConditionDefinitionTests
    {
        [TestMethod]
        public void Constructor_NullConditionsThrows()
        {
            // Act + Assert
            Assert.ThrowsException<ArgumentNullException>(() => new CompositeConditionDefinition(null));
        }

        [TestMethod]
        public void Constructor_EmptyConditionsThrows()
        {
            // Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new CompositeConditionDefinition(new PropertyConditionDefinition[0]));
        }

        [DataRow(1, DisplayName = "ValidConditions_Single")]
        [DataRow(3, DisplayName = "ValidConditions_Multiple")]
        [DataTestMethod]
        public void Constructor_ValidConditionsAreSet(int conditionsCount)
        {
            // Arrange
            var expectedConditions = new List<PropertyConditionDefinition>();
            for (int i = 0; i < conditionsCount; ++i)
            {
                expectedConditions.Add(new PropertyConditionDefinition($"TestProperty{i}", new LiteralValueDefinition($"TestValue{i}")));
            }

            // Act
            var compositeConditionDefinition = new CompositeConditionDefinition(expectedConditions);

            // Assert
            Assert.AreEqual(expectedConditions, compositeConditionDefinition.Conditions);
        }
    }
}
