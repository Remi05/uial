using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Parser.UnitTests
{
    [TestClass]
    public class ContextDeclarationsParsingTests
    {
        private const string ContextIdentifier = "context";
        private const string ContextName = "TestContext";
        private const string SingleParam = "$testParam";
        private const string Params = "$testParam1, $testParam2";
        private const string SingleCondition = "Name=\"TestName\"";
        private const string CompositeCondition = "Name=\"TestName\", ControlType=\"Button\"";

        private const string Context_NoParamsNoCondition             = ContextIdentifier + " " + ContextName + ":";
        private const string Context_SingleParam                     = ContextIdentifier + " " + ContextName + "(" + SingleParam + "):";
        private const string Context_MultipleParams                  = ContextIdentifier + " " + ContextName + "(" + Params + "):";
        private const string Context_SingleRootCondition             = ContextIdentifier + " " + ContextName + " [" + SingleCondition + "]:";
        private const string Context_CompositeRootCondition          = ContextIdentifier + " " + ContextName + " [" + CompositeCondition + "]:";
        private const string Context_SingleUniqueCondition           = ContextIdentifier + " " + ContextName + " {" + SingleCondition + "}:";
        private const string Context_CompositeUniqueCondition        = ContextIdentifier + " " + ContextName + " {" + CompositeCondition + "}:";
        private const string Context_RootAndUniqueCondition          = ContextIdentifier + " " + ContextName + " [" + CompositeCondition + "] {" + CompositeCondition + "}:";
        private const string Context_ParamsAndRootCondition          = ContextIdentifier + " " + ContextName + "(" + Params + ") [" + CompositeCondition + "]:";
        private const string Context_ParamsAndUniqueCondition        = ContextIdentifier + " " + ContextName + "(" + Params + ") {" + CompositeCondition + "}:";
        private const string Context_ParamsAndRootAndUniqueCondition = ContextIdentifier + " " + ContextName + "(" + Params + ") [" + CompositeCondition + "] {" + CompositeCondition + "}:";

        [TestMethod]
        public void ContextNameIsParsed()
        {
            ScriptParser parser = new ScriptParser();
            IContextDefinition contextDefinition = parser.ParseContextDefinitionDeclaration(null, Context_SingleRootCondition);

            Assert.IsNotNull(contextDefinition, "The parsed IContextDefinition should not be null.");
            Assert.AreEqual(ContextName, contextDefinition.Name, "The parsed IContextDefinition's Name should be the given name.");
        }

        [DataRow(Context_NoParamsNoCondition, DisplayName = "ContextCanBeParsed_NoParamsNoCondition")]
        [DataRow(Context_SingleParam, DisplayName = "ContextCanBeParsed_SingleParam")]
        [DataRow(Context_MultipleParams, DisplayName = "ContextCanBeParsed_MultipleParams")]
        [DataRow(Context_SingleRootCondition, DisplayName = "ContextCanBeParsed_SingleRootCondition")]
        [DataRow(Context_CompositeRootCondition, DisplayName = "ContextCanBeParsed_CompositeRootCondition")]
        [DataRow(Context_SingleUniqueCondition, DisplayName = "ContextCanBeParsed_SingleUniqueCondition")]
        [DataRow(Context_CompositeUniqueCondition, DisplayName = "ContextCanBeParsed_CompositeUniqueCondition")]
        [DataRow(Context_RootAndUniqueCondition, DisplayName = "ContextCanBeParsed_RootAndUniqueCondition")]
        [DataRow(Context_ParamsAndRootCondition, DisplayName = "ContextCanBeParsed_ParamsAndRootCondition")]
        [DataRow(Context_ParamsAndUniqueCondition, DisplayName = "ContextCanBeParsed_ParamsAndUniqueCondition")]
        [DataRow(Context_ParamsAndRootAndUniqueCondition, DisplayName = "ContextCanBeParsed_ParamsAndRootAndUniqueCondition")]
        [DataTestMethod]
        public void ContextCanBeParsed(string contextDeclaration)
        {
            ScriptParser parser = new ScriptParser();
            IContextDefinition contextDefinition = parser.ParseContextDefinitionDeclaration(null, contextDeclaration);

            Assert.IsNotNull(contextDefinition, "The parsed IContextDefinition should not be null.");
        }
    }
}
