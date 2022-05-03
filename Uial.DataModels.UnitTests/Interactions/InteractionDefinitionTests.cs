using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Uial.DataModels.UnitTests
{
    [TestClass]
    public class InteractionDefinitionTests
    {
        [DataRow("", DisplayName = "ValidName_EmptyString")]
        [DataRow("TestInteraction", DisplayName = "ValidName_SampleName")]
        [DataTestMethod]
        public void Constructor_ValidNameIsSet(string expectedName)
        {
            // Act
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), expectedName, new List<string>(), new List<BaseInteractionDefinition>());

            // Assert
            Assert.AreEqual(expectedName, interactionDefinition.Name);
        }

        [TestMethod]
        public void Constructor_NullNameThrows()
        {
            // Act + Assert
            Assert.ThrowsException<ArgumentNullException>(() => new InteractionDefinition(new DefinitionScope(), null, new List<string>(), new List<BaseInteractionDefinition>()));
        }

        [TestMethod]
        public void Constructor_NullDefinitionScopeIsSet()
        {
            // Act
            var interactionDefinition = new InteractionDefinition(null, "TestInteraction", new List<string>(), new List<BaseInteractionDefinition>());

            // Assert
            Assert.IsNull(interactionDefinition.Scope);
        }

        [TestMethod]
        public void Constructor_ValidDefinitionScopeIsSet()
        {
            // Arrange
            var expectedDefinitionScope = new DefinitionScope();

            // Act
            var interactionDefinition = new InteractionDefinition(expectedDefinitionScope, "TestInteraction", new List<string>(), new List<BaseInteractionDefinition>());

            // Assert
            Assert.AreEqual(expectedDefinitionScope, interactionDefinition.Scope);
        }

        [DataRow(null, DisplayName = "ValidParamNames_Null")]
        [DataRow(new string[0], DisplayName = "ValidParamNames_Empty")]
        [DataRow(new string[] { "TestParam" }, DisplayName = "ValidParamNames_SingleParam")]
        [DataRow(new string[] { "TestParam1", "TestParam2", "TestParam3" }, DisplayName = "ValidParamNames_MultipleParams")]
        [DataTestMethod]
        public void Constructor_ValidParamNamesAreSet(string[] expectedParamNames)
        {
            // Act
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "TestInteraction", expectedParamNames, new List<BaseInteractionDefinition>());

            // Assert
            Assert.AreEqual(expectedParamNames, interactionDefinition.ParamNames);
        }

        [TestMethod]
        public void Constructor_NullBaseInteractionDefinitionsIsSet()
        {
            // Act
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "TestInteraction", new List<string>(), null);

            // Assert
            Assert.IsNull(interactionDefinition.BaseInteractionDefinitions);
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
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "TestInteraction", new List<string>(), expectedBaseInteractionDefinitions);

            // Assert
            Assert.AreEqual(expectedBaseInteractionDefinitions, interactionDefinition.BaseInteractionDefinitions);
        }
    }
}
