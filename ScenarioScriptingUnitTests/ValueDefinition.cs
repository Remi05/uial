using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScenarioScripting;
using ScenarioScripting.Interactions;
using ScenarioScripting.Scenarios;
using ScenarioScripting.Scopes;
using ScenarioScriptingUnitTests.Interactions;

namespace ScenarioScriptingUnitTests.Scenarios
{
    [TestClass]
    public class ValueDefinitionTests
    {
        [TestMethod]
        public void VerifyLitteralValueIsResolved()
        {
            string expectedValue = "TestLitteralValue";

            var valueDefinition = ValueDefinition.FromLitteral(expectedValue);
            string actualValue = valueDefinition.Resolve(null);

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void VerifyReferenceValueIsResolved()
        {
            string referenceName = "TestReferenceName";
            string expectedValue = "TestReferenceValue";

            var referenceValues = new Dictionary<string, string>() { { referenceName, expectedValue } };
            var definitionScope = new DefinitionScope();
            var runtimeScope = new RuntimeScope(definitionScope, referenceValues);

            var valueDefinition = ValueDefinition.FromReference(referenceName);
            string actualValue = valueDefinition.Resolve(runtimeScope);

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
