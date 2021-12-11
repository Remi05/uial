using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.DataModels;
using Uial.Values;

namespace Uial.UnitTests.Values
{
    [TestClass]
    public class ValueResolverTests
    {
        [TestMethod]
        public void LiteralsAreResolvedAsTheirValue()
        {
            object expectedValue = "TestLiteralValue";

            var valueDefinition = new LiteralValueDefinition(expectedValue);
            var valueResolver = new ValueResolver();
            object actualValue = valueResolver.Resolve(valueDefinition, new ReferenceValueStore());

            Assert.AreEqual(expectedValue, actualValue, "Literal - Resolve(scope) should return the given literal value.");
        }

        [TestMethod]
        public void ReferencesAreResolvedAsValueSetInScope()
        {
            string referenceName = "TestReferenceName";
            object expectedValue = "TestReferenceValue";

            var referenceValueStore = new ReferenceValueStore();
            referenceValueStore.SetValue(referenceName, expectedValue);

            var valueDefinition = new ReferenceValueDefinition(referenceName);
            var valueResolver = new ValueResolver();
            object actualValue = valueResolver.Resolve(valueDefinition, referenceValueStore);

            Assert.AreEqual(expectedValue, actualValue, "Reference - Resolve(scope) should return the value set in the given scope.");
        }

        [TestMethod]
        public void LiteralsCanBeResolvedWithNullValueStore()
        {
            object expectedValue = "TestLiteralValue";

            ValueDefinition valueDefinition = new LiteralValueDefinition(expectedValue);
            var valueResolver = new ValueResolver();
            object actualValue = valueResolver.Resolve(valueDefinition, null);

            Assert.AreEqual(expectedValue, actualValue, "Literal - Resolve(null) should not throw and return the given literal value.");
        }

        [TestMethod]
        public void ReferencesThrowWhenValueStoreIsNull()
        {
            string referenceName = "TestReferenceName";
            var valueDefinition = new ReferenceValueDefinition(referenceName);
            var valueResolver = new ValueResolver();

            Assert.ThrowsException<NullReferenceException>(() => valueResolver.Resolve(valueDefinition, null), "Reference - Resolve(null) should throw.");
        }
    }
}
