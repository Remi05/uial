using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.DataModels;
using Uial.Interactions;
using Uial.Scopes;
using Uial.Values;
using Uial.UnitTests.Contexts;

namespace Uial.UnitTests.Interactions
{
    [TestClass]
    public class InteractionResolverTests
    {
        [TestMethod]
        public void VerifyResolvedInteractionHasSameName()
        {
            // Arrange
            string expectedName = "TestInteractionDefinition";

            var runtimeScope = new RuntimeScope(new DefinitionScope(), new ReferenceValueStore());
            var parentContext = new MockContext(runtimeScope);
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), expectedName, null,null);
            var interactionResolver = new InteractionResolver(null);
            
            // Act
            IInteraction interaction = interactionResolver.Resolve(interactionDefinition, null, parentContext);
            
            // Assert
            Assert.AreEqual(expectedName, interaction.Name);
        }

        [TestMethod]
        public void VerifyResolvingNullInteractionDefinitionThrows()
        {
            // Arrange
            var runtimeScope = new RuntimeScope(new DefinitionScope(), new ReferenceValueStore());
            var parentContext = new MockContext(runtimeScope);
            var interactionResolver = new InteractionResolver(null);

            // Act + Assert
            Assert.ThrowsException<ArgumentNullException>(() => interactionResolver.Resolve(null, new List<object>(), parentContext));
        }

        [TestMethod]
        public void VerifyResolvingWithNullContextThrows()
        {
            // Arrange
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "", null, null);
            var interactionResolver = new InteractionResolver(null);

            // Act + Assert
            Assert.ThrowsException<ArgumentNullException>(() => interactionResolver.Resolve(interactionDefinition, new List<object>(), null));
        }

        [TestMethod]
        public void VerifyResolvingWithMissingParamValuesThrows()
        {
            // Arrange
            var paramNames = new List<string>() { "TestParamName1" };
            var paramValues = new List<object>();

            var runtimeScope = new RuntimeScope(new DefinitionScope(), new ReferenceValueStore());
            var parentContext = new MockContext(runtimeScope);
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "", paramNames, null);
            var interactionResolver = new InteractionResolver(null);

            // Act + Assert
            Assert.ThrowsException<InvalidParameterCountException>(() => interactionResolver.Resolve(interactionDefinition, paramValues, parentContext));
        }

        [TestMethod]
        public void VerifyResolvingWithExtraParamValuesThrows()
        {
            // Arrange
            var paramNames = new List<string>();
            var paramValues = new List<object>() { "TestParamValue1" };

            var runtimeScope = new RuntimeScope(new DefinitionScope(), new ReferenceValueStore());
            var parentContext = new MockContext(runtimeScope);
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "", paramNames, null);
            var interactionResolver = new InteractionResolver(null);

            // Act + Assert
            Assert.ThrowsException<InvalidParameterCountException>(() => interactionResolver.Resolve(interactionDefinition, paramValues, parentContext));
        }

        [TestMethod]
        public void VerifyValueStoreIsPopulatedWithParamValues()
        {
            // Arrange
            var expectedParamValues = new Dictionary<string, object>()
            {
                { "ParamName1", "ParamValue1" },
                { "ParamName2", 254 },
                { "ParamName3", 35.0f },
            };

            var baseInteractionDefinitions = new List<BaseInteractionDefinition>() {
                new BaseInteractionDefinition("MockInteraction1"),
                new BaseInteractionDefinition("MockInteraction2"),
            };

            var mockBaseInteractionResolver = new MockBaseInteractionResolver();
            var interactionResolver = new InteractionResolver(mockBaseInteractionResolver);
            
            var runtimeScope = new RuntimeScope(new DefinitionScope(), new ReferenceValueStore());
            var parentContext = new MockContext(runtimeScope);
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "", expectedParamValues.Keys.ToList(), baseInteractionDefinitions);

            // Act
            interactionResolver.Resolve(interactionDefinition, expectedParamValues.Values.ToList(), parentContext);
            var passedValueStore = mockBaseInteractionResolver.LastPassedValueStore;

            // Assert
            foreach (string paramName in expectedParamValues.Keys)
            {
                object expectedParamValue = expectedParamValues[paramName];
                object actualParamValue = passedValueStore.GetValue(paramName);
                Assert.AreEqual(expectedParamValue, actualParamValue);
            }
        }

        // TODO: Add test around resolved interactions being passed to CompositeInteraction constructor.

        [TestMethod]
        public void VerifyBaseInteractionDefinitionsAreResolvedWithBaseInteractionResolver()
        {
            // Arrange
            var expectedBaseInteractionDefinitions = new List<BaseInteractionDefinition>() {
                new BaseInteractionDefinition("MockInteraction1"),
                new BaseInteractionDefinition("MockInteraction2"),
                new BaseInteractionDefinition("MockInteraction3"),
            };

            var mockBaseInteractionResolver = new MockBaseInteractionResolver();
            var interactionResolver = new InteractionResolver(mockBaseInteractionResolver);

            var runtimeScope = new RuntimeScope(new DefinitionScope(), new ReferenceValueStore());
            var parentContext = new MockContext(runtimeScope);
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "", null, expectedBaseInteractionDefinitions);

            // Act
            interactionResolver.Resolve(interactionDefinition, new List<object>(), parentContext);
            var actualBaseInteractionDefinitions = mockBaseInteractionResolver.ResolvedBaseInteractions;

            // Assert
            Assert.IsTrue(expectedBaseInteractionDefinitions.SequenceEqual(actualBaseInteractionDefinitions));
        }

        [TestMethod]
        public void VerifyResolvedInteractionExecutesBaseInteractions()
        {
            // Arrange
            var expectedInteractions = new List<string>() {
                "TestInteraction1",
                "TestInteraction2",
                "TestInteraction3",
            };

            var expectedBaseInteractionDefinitions = new List<BaseInteractionDefinition>() {
                new BaseInteractionDefinition("MockInteraction1"),
                new BaseInteractionDefinition("MockInteraction2"),
                new BaseInteractionDefinition("MockInteraction3"),
            };

            var mockBaseInteractionResolver = new MockBaseInteractionResolver();
            var interactionResolver = new InteractionResolver(mockBaseInteractionResolver);

            var runtimeScope = new RuntimeScope(new DefinitionScope(), new ReferenceValueStore());
            var parentContext = new MockContext(runtimeScope);
            var interactionDefinition = new InteractionDefinition(new DefinitionScope(), "", new List<string>(), expectedBaseInteractionDefinitions);

            // Act
            interactionResolver.Resolve(interactionDefinition, new List<object>(), parentContext);
            var actualBaseInteractionDefinitions = mockBaseInteractionResolver.ResolvedBaseInteractions;

            // Assert
            Assert.IsTrue(expectedBaseInteractionDefinitions.SequenceEqual(actualBaseInteractionDefinitions));
        }
    }
}
