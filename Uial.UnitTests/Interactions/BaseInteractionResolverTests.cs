using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Interactions;
using Uial.UnitTests.Contexts;
using Uial.UnitTests.Values;

namespace Uial.UnitTests.Interactions
{
    [TestClass]
    public class BaseInteractionResolverTests
    {
        [TestMethod]
        public void ArgumentNullExceptionIsThrownForNullBaseInteractionDefinition()
        {
            var baseInteractionResolver = new BaseInteractionResolver(null, null, null);
            Assert.ThrowsException<ArgumentNullException>(() => baseInteractionResolver.Resolve(null, new MockContext(), null));
        }

        // Define what happens when a null BaseContexDefinition is passed

        // Define what happens when the BaseContextResolver returns null

        [TestMethod]
        public void InteractionIsObtainedFromInteractionProvider()
        {
            // Arrange
            string interactionName = "TestInteraction";
            var expectedInteraction = new MockInteraction(interactionName);
            var baseInteractionDefinition = new BaseInteractionDefinition(interactionName);

            var mockInteractionProvider = new MockInteractionProvider();
            mockInteractionProvider.InteractionsMap[interactionName] = expectedInteraction;

            // Act
            var baseInteractionResolver = new BaseInteractionResolver(mockInteractionProvider, null, null);
            IInteraction actualInteraction = baseInteractionResolver.Resolve(baseInteractionDefinition, null, null);

            // Assert
            Assert.AreEqual(expectedInteraction, actualInteraction);
        }

        [TestMethod]
        public void InteractionParamsAreResolvedFromValueResolver()
        {
            // Arrange
            var expectedParamValues = new Dictionary<ValueDefinition, object>()
            {
                { new ReferenceValueDefinition("$testRefValue1"), "test value" },
                { new ReferenceValueDefinition("$testRefValue2"), 15 },
                { new ReferenceValueDefinition("$testRefValue3"), 25.0f },
            };

            string interactionName = "TestInteraction";
            var baseInteractionDefinition = new BaseInteractionDefinition(interactionName, expectedParamValues.Keys);
            var mockInteractionProvider = new MockInteractionProvider();
            mockInteractionProvider.InteractionsMap[interactionName] = null;

            var mockValueResolver = new MockValueResolver(expectedParamValues);

            // Act
            var baseInteractionResolver = new BaseInteractionResolver(mockInteractionProvider, null, mockValueResolver);
            baseInteractionResolver.Resolve(baseInteractionDefinition, null, null);

            // Assert
            Assert.IsTrue(expectedParamValues.Values.SequenceEqual(mockInteractionProvider.PassedParamValues));
        }

        [TestMethod]
        public void ContextIsResolvedFromBaseContextResolver()
        {
            // Arrange
            string interactionName = "TestInteraction";
            var expectedContext = new MockContext(name: "TestContext");
            var baseContextDefinition = new BaseContextDefinition(new ContextScopeDefinition("TestContextDefinition"));
            var baseInteractionDefinition = new BaseInteractionDefinition(interactionName, contextDefinition: baseContextDefinition);
            var mockInteractionProvider = new MockInteractionProvider();
            mockInteractionProvider.InteractionsMap[interactionName] = null;

            var mockBaseContextResolver = new MockBaseContextResolver();
            mockBaseContextResolver.ContextsMap[baseContextDefinition] = expectedContext;

            // Act
            var baseInteractionResolver = new BaseInteractionResolver(mockInteractionProvider, mockBaseContextResolver, null);
            baseInteractionResolver.Resolve(baseInteractionDefinition, null, null);

            IContext actualContext = mockInteractionProvider.PassedContext;

            // Assert
            Assert.AreEqual(expectedContext, actualContext);
        }
    }
}

