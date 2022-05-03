using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Uial.DataModels.UnitTests
{
    [TestClass]
    public class ScenarioDefinitionTests
    {
        [DataRow("", DisplayName = "ValidName_EmptyString")]
        [DataRow("TestScenario", DisplayName = "ValidName_SampleName")]
        [DataTestMethod]
        public void Constructor_ValidNameIsSet(string expectedName)
        {
            // Act
            var scenarioDefinition = new ScenarioDefinition(expectedName, new List<BaseInteractionDefinition>());

            // Assert
            Assert.AreEqual(expectedName, scenarioDefinition.Name);
        }

        [TestMethod]
        public void Constructor_NullNameThrows()
        {
            // Act + Assert
            Assert.ThrowsException<ArgumentNullException>(() => new ScenarioDefinition(null, new List<BaseInteractionDefinition>()));
        }

        [TestMethod]
        public void Constructor_NullBaseInteractionDefinitionsIsSet()
        {
            // Act
            var scenarioDefinition = new ScenarioDefinition("TestInteraction", null);

            // Assert
            Assert.IsNull(scenarioDefinition.BaseInteractionDefinitions);
        }

        [DataRow(0, DisplayName = "ValidBaseInteractionDefinitions_Empty")]
        [DataRow(1, DisplayName = "ValidBaseInteractionDefinitions_Single")]
        [DataRow(5, DisplayName = "ValidBaseInteractionDefinitions_Multiple")]
        [DataTestMethod]
        public void Constructor_NullBaseInteractionDefinitionsIsSet(int baseInteractionDefinitionsCount)
        {
            // Arrange
            var expectedBaseInteractionDefinitions = new List<BaseInteractionDefinition>();
            for (int i = 0; i < baseInteractionDefinitionsCount; ++i)
            {
                expectedBaseInteractionDefinitions.Add(new BaseInteractionDefinition($"BaseInteraction{i}"));
            }

            // Act
            var scenarioDefinition = new ScenarioDefinition("TestInteraction", expectedBaseInteractionDefinitions);

            // Assert
            Assert.AreEqual(expectedBaseInteractionDefinitions, scenarioDefinition.BaseInteractionDefinitions);
        }
    }
}
