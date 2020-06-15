using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Contexts;
using Uial.Parsing.Exceptions;

namespace Uial.Parsing.UnitTests
{
    [TestClass]
    public class ContextDeclarationsParsingTests
    {
        private const string ContextIdentifier = "context";
        private const string ContextName = "TestContext";
        private const string Param = "$testParam";
        private const string Params = "$testParam1, $testParam2";
        private const string SingleCondition = "Name=\"TestName\"";
        private const string CompositeCondition = "Name=\"TestName\", ControlType=\"Button\"";

        private class ValidContexts
        {
            public const string NoParamsNoCondition             = ContextIdentifier + " " + ContextName + ":";
            public const string SingleParam                     = ContextIdentifier + " " + ContextName + "(" + Param + "):";
            public const string MultipleParams                  = ContextIdentifier + " " + ContextName + "(" + Params + "):";
            public const string SingleRootCondition             = ContextIdentifier + " " + ContextName + " [" + SingleCondition + "]:";
            public const string CompositeRootCondition          = ContextIdentifier + " " + ContextName + " [" + CompositeCondition + "]:";
            public const string SingleUniqueCondition           = ContextIdentifier + " " + ContextName + " {" + SingleCondition + "}:";
            public const string CompositeUniqueCondition        = ContextIdentifier + " " + ContextName + " {" + CompositeCondition + "}:";
            public const string RootAndUniqueCondition          = ContextIdentifier + " " + ContextName + " [" + CompositeCondition + "] {" + CompositeCondition + "}:";
            public const string ParamsAndRootCondition          = ContextIdentifier + " " + ContextName + "(" + Params + ") [" + CompositeCondition + "]:";
            public const string ParamsAndUniqueCondition        = ContextIdentifier + " " + ContextName + "(" + Params + ") {" + CompositeCondition + "}:";
            public const string ParamsAndRootAndUniqueCondition = ContextIdentifier + " " + ContextName + "(" + Params + ") [" + CompositeCondition + "] {" + CompositeCondition + "}:";
        }

        private class InvalidContexts
        {
            public const string MissingName                = ContextIdentifier + " :";
            public const string MultipleParamsDeclarations = ContextIdentifier + " " + ContextName + "(" + Params + ") (" + Params + "):";
            public const string MultipleRootConditions     = ContextIdentifier + " " + ContextName + " [" + SingleCondition + "] [" + SingleCondition + "]:";
            public const string MultipleUniqueConditions   = ContextIdentifier + " " + ContextName + " {" + SingleCondition + "} {" + SingleCondition + "}:";
            public const string ParamsAfterRootCondition   = ContextIdentifier + " " + ContextName + " [" + SingleCondition + "] (" + Params + "):";
            public const string ParamsAfterUniqueCondition = ContextIdentifier + " " + ContextName + " {" + SingleCondition + "} (" + Params + "):";
            public const string TextAfterColon             = ContextIdentifier + " " + ContextName + ": SomeTestText";
        }


        [TestMethod]
        public void ContextNameIsParsed()
        {
            ScriptParser parser = new ScriptParser();
            IContextDefinition contextDefinition = parser.ParseContextDefinitionDeclaration(null, ValidContexts.SingleRootCondition);

            Assert.IsNotNull(contextDefinition, "The parsed IContextDefinition should not be null.");
            Assert.AreEqual(ContextName, contextDefinition.Name, "The parsed IContextDefinition's Name should be the given name.");
        }


        [DataRow(ValidContexts.NoParamsNoCondition,             DisplayName = "ValidContext_NoParamsNoCondition")]
        [DataRow(ValidContexts.SingleParam,                     DisplayName = "ValidContext_SingleParam")]
        [DataRow(ValidContexts.MultipleParams,                  DisplayName = "ValidContext_MultipleParams")]
        [DataRow(ValidContexts.SingleRootCondition,             DisplayName = "ValidContext_SingleRootCondition")]
        [DataRow(ValidContexts.CompositeRootCondition,          DisplayName = "ValidContext_CompositeRootCondition")]
        [DataRow(ValidContexts.SingleUniqueCondition,           DisplayName = "ValidContext_SingleUniqueCondition")]
        [DataRow(ValidContexts.CompositeUniqueCondition,        DisplayName = "ValidContext_CompositeUniqueCondition")]
        [DataRow(ValidContexts.RootAndUniqueCondition,          DisplayName = "ValidContext_RootAndUniqueCondition")]
        [DataRow(ValidContexts.ParamsAndRootCondition,          DisplayName = "ValidContext_ParamsAndRootCondition")]
        [DataRow(ValidContexts.ParamsAndUniqueCondition,        DisplayName = "ValidContext_ParamsAndUniqueCondition")]
        [DataRow(ValidContexts.ParamsAndRootAndUniqueCondition, DisplayName = "ValidContext_ParamsAndRootAndUniqueCondition")]
        [DataTestMethod]
        public void ValidContextsCanBeParsed(string contextDeclaration)
        {
            ScriptParser parser = new ScriptParser();
            IContextDefinition contextDefinition = parser.ParseContextDefinitionDeclaration(null, contextDeclaration);

            Assert.IsNotNull(contextDefinition, "The parsed IContextDefinition should not be null.");
        }


        [DataRow(InvalidContexts.MissingName,                DisplayName = "InvalidContext_MissingName")]
        [DataRow(InvalidContexts.MultipleParamsDeclarations, DisplayName = "InvalidContext_MultipleParamsDeclarations")]
        [DataRow(InvalidContexts.MultipleRootConditions,     DisplayName = "InvalidContext_MultipleRootConditions")]
        [DataRow(InvalidContexts.MultipleUniqueConditions,   DisplayName = "InvalidContext_MultipleUniqueConditions")]
        [DataRow(InvalidContexts.ParamsAfterRootCondition,   DisplayName = "InvalidContext_ParamsAfterRootCondition")]
        [DataRow(InvalidContexts.ParamsAfterUniqueCondition, DisplayName = "InvalidContext_ParamsAfterUniqueCondition")]
        [DataRow(InvalidContexts.TextAfterColon,             DisplayName = "InvalidContext_TextAfterColon")]
        [DataTestMethod]
        public void InvalidContextsThrowException(string contextDeclaration)
        {
            ScriptParser parser = new ScriptParser();
            Assert.ThrowsException<InvalidContextDeclarationException>(() => parser.ParseContextDefinitionDeclaration(null, contextDeclaration));
        }
    }
}
