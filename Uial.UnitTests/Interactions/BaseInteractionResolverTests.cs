using System;
using System.Collections.Generic;
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
            object expectedParamValue = "test value";
            string interactionName = "TestInteraction";
            ValueDefinition valueDefinition = new ReferenceValueDefinition("$testRefValue");
            var paramsDefinitions = new List<ValueDefinition>() { valueDefinition };
            var baseInteractionDefinition = new BaseInteractionDefinition(interactionName, paramsDefinitions);
            var mockInteractionProvider = new MockInteractionProvider();
            mockInteractionProvider.InteractionsMap[interactionName] = null;

            var mockValueResolver = new MockValueResolver();
            mockValueResolver.ValuesMap[valueDefinition] = expectedParamValue;

            // Act
            var baseInteractionResolver = new BaseInteractionResolver(mockInteractionProvider, null, mockValueResolver);
            baseInteractionResolver.Resolve(baseInteractionDefinition, null, null);

            object actualParamValue = mockInteractionProvider.PassedParamValues.GetEnumerator().Current;

            // Assert
            Assert.AreEqual(expectedParamValue, actualParamValue);
        }

        [TestMethod]
        public void ContextIsResolvedFromBaseContextResolver()
        {
            // Arrange
            string interactionName = "TestInteraction";
            var expectedContext = new MockContext(name: "TestContext");
            var baseContextDefinition = new BaseContextDefinition(new ContextScopeDefinition("TestContextDefinition"));
            var baseInteractionDefinition = new BaseInteractionDefinition(interactionName);
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

