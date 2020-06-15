using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Scopes;

namespace Uial.UnitTests.Scenarios
{
    [TestClass]
    public class ValueDefinitionTests
    {
        [TestMethod]
        public void FromLitteralThrowsWhenLitteralIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ValueDefinition.FromLitteral(null), "FromLitteral() should throw if the given litteral is null.");
        }

        [TestMethod]
        public void FromReferenceThrowsWhenReferenceNameIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ValueDefinition.FromReference(null), "FromReference() should throw if the given reference name is null.");
        }

        [TestMethod]
        public void LitteralsAreResolvedAsTheirValue()
        {
            string expectedValue = "TestLitteralValue";
            var runtimeScope = new RuntimeScope(new DefinitionScope(), new Dictionary<string, string>());

            var valueDefinition = ValueDefinition.FromLitteral(expectedValue);
            string actualValue = valueDefinition.Resolve(runtimeScope);

            Assert.AreEqual(expectedValue, actualValue, "Litteral - Resolve(scope) should return the given litteral value.");
        }

        [TestMethod]
        public void ReferencesAreResolvedAsValueSetInScope()
        {
            string referenceName = "TestReferenceName";
            string expectedValue = "TestReferenceValue";
            var referenceValues = new Dictionary<string, string>() { { referenceName, expectedValue } };
            var runtimeScope = new RuntimeScope(new DefinitionScope(), referenceValues);

            var valueDefinition = ValueDefinition.FromReference(referenceName);
            string actualValue = valueDefinition.Resolve(runtimeScope);

            Assert.AreEqual(expectedValue, actualValue, "Reference - Resolve(scope) should return the value set in the given scope.");
        }

        [TestMethod]
        public void LitteralsCanBeResolvedWithNullScope()
        {
            string expectedValue = "TestLitteralValue";

            ValueDefinition valueDefinition = ValueDefinition.FromLitteral(expectedValue);
            string actualValue = valueDefinition.Resolve(null);

            Assert.AreEqual(expectedValue, actualValue, "Litteral - Resolve(null) should not throw and return the given litteral value.");
        }

        [TestMethod]
        public void ReferencesThrowWhenScopeIsNull()
        {
            string referenceName = "TestReferenceName";
            var valueDefinition = ValueDefinition.FromReference(referenceName);

            Assert.ThrowsException<ReferenceValueNotFoundException>(() => valueDefinition.Resolve(null), "Reference - Resolve(null) should throw.");
        }
    }
}
