using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScenarioScripting.Contexts;
using ScenarioScripting.Parser.Exceptions;

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

        private const string ValidContext_NoParamsNoCondition             = ContextIdentifier + " " + ContextName + ":";
        private const string ValidContext_SingleParam                     = ContextIdentifier + " " + ContextName + "(" + SingleParam + "):";
        private const string ValidContext_MultipleParams                  = ContextIdentifier + " " + ContextName + "(" + Params + "):";
        private const string ValidContext_SingleRootCondition             = ContextIdentifier + " " + ContextName + " [" + SingleCondition + "]:";
        private const string ValidContext_CompositeRootCondition          = ContextIdentifier + " " + ContextName + " [" + CompositeCondition + "]:";
        private const string ValidContext_SingleUniqueCondition           = ContextIdentifier + " " + ContextName + " {" + SingleCondition + "}:";
        private const string ValidContext_CompositeUniqueCondition        = ContextIdentifier + " " + ContextName + " {" + CompositeCondition + "}:";
        private const string ValidContext_RootAndUniqueCondition          = ContextIdentifier + " " + ContextName + " [" + CompositeCondition + "] {" + CompositeCondition + "}:";
        private const string ValidContext_ParamsAndRootCondition          = ContextIdentifier + " " + ContextName + "(" + Params + ") [" + CompositeCondition + "]:";
        private const string ValidContext_ParamsAndUniqueCondition        = ContextIdentifier + " " + ContextName + "(" + Params + ") {" + CompositeCondition + "}:";
        private const string ValidContext_ParamsAndRootAndUniqueCondition = ContextIdentifier + " " + ContextName + "(" + Params + ") [" + CompositeCondition + "] {" + CompositeCondition + "}:";

        private const string InvalidContext_MissingName                = ContextIdentifier + " :";
        private const string InvalidContext_MultipleParamsDeclarations = ContextIdentifier + " " + ContextName + "(" + Params + ") (" + Params + "):";
        private const string InvalidContext_MultipleRootConditions     = ContextIdentifier + " " + ContextName + " [" + SingleCondition + "] [" + SingleCondition + "]:";
        private const string InvalidContext_MultipleUniqueConditions   = ContextIdentifier + " " + ContextName + " {" + SingleCondition + "} {" + SingleCondition + "}:";
        private const string InvalidContext_ParamsAfterRootCondition   = ContextIdentifier + " " + ContextName + " [" + SingleCondition + "] (" + Params + "):";
        private const string InvalidContext_ParamsAfterUniqueCondition = ContextIdentifier + " " + ContextName + " {" + SingleCondition + "} (" + Params + "):";
        private const string InvalidContext_TextAfterColon             = ContextIdentifier + " " + ContextName + ": SomeTestText";


        [TestMethod]
        public void ContextNameIsParsed()
        {
            ScriptParser parser = new ScriptParser();
            IContextDefinition contextDefinition = parser.ParseContextDefinitionDeclaration(null, ValidContext_SingleRootCondition);

            Assert.IsNotNull(contextDefinition, "The parsed IContextDefinition should not be null.");
            Assert.AreEqual(ContextName, contextDefinition.Name, "The parsed IContextDefinition's Name should be the given name.");
        }


        [DataRow(ValidContext_NoParamsNoCondition, DisplayName = "ValidContext_NoParamsNoCondition")]
        [DataRow(ValidContext_SingleParam, DisplayName = "ValidContext_SingleParam")]
        [DataRow(ValidContext_MultipleParams, DisplayName = "ValidContext_MultipleParams")]
        [DataRow(ValidContext_SingleRootCondition, DisplayName = "ValidContext_SingleRootCondition")]
        [DataRow(ValidContext_CompositeRootCondition, DisplayName = "ValidContext_CompositeRootCondition")]
        [DataRow(ValidContext_SingleUniqueCondition, DisplayName = "ValidContext_SingleUniqueCondition")]
        [DataRow(ValidContext_CompositeUniqueCondition, DisplayName = "ValidContext_CompositeUniqueCondition")]
        [DataRow(ValidContext_RootAndUniqueCondition, DisplayName = "ValidContext_RootAndUniqueCondition")]
        [DataRow(ValidContext_ParamsAndRootCondition, DisplayName = "ValidContext_ParamsAndRootCondition")]
        [DataRow(ValidContext_ParamsAndUniqueCondition, DisplayName = "ValidContext_ParamsAndUniqueCondition")]
        [DataRow(ValidContext_ParamsAndRootAndUniqueCondition, DisplayName = "ValidContext_ParamsAndRootAndUniqueCondition")]
        [DataTestMethod]
        public void ValidContextsCanBeParsed(string contextDeclaration)
        {
            ScriptParser parser = new ScriptParser();
            IContextDefinition contextDefinition = parser.ParseContextDefinitionDeclaration(null, contextDeclaration);

            Assert.IsNotNull(contextDefinition, "The parsed IContextDefinition should not be null.");
        }


        [DataRow(InvalidContext_MissingName, DisplayName = "InvalidContext_MissingName")]
        [DataRow(InvalidContext_MultipleParamsDeclarations, DisplayName = "InvalidContext_MultipleParamsDeclarations")]
        [DataRow(InvalidContext_MultipleRootConditions, DisplayName = "InvalidContext_MultipleRootConditions")]
        [DataRow(InvalidContext_MultipleUniqueConditions, DisplayName = "InvalidContext_MultipleUniqueConditions")]
        [DataRow(InvalidContext_ParamsAfterRootCondition, DisplayName = "InvalidContext_ParamsAfterRootCondition")]
        [DataRow(InvalidContext_ParamsAfterUniqueCondition, DisplayName = "InvalidContext_ParamsAfterUniqueCondition")]
        [DataRow(InvalidContext_TextAfterColon, DisplayName = "InvalidContext_TextAfterColon")]
        [DataTestMethod]
        public void InvalidContextsThrowException(string contextDeclaration)
        {
            ScriptParser parser = new ScriptParser();
            Assert.ThrowsException<InvalidContextDeclarationException>(() => parser.ParseContextDefinitionDeclaration(null, contextDeclaration));
        }
    }
}
