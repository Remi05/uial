using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.DataModels;
using Uial.Parsing.Exceptions;

namespace Uial.Parsing.UnitTests
{
    [TestClass]
    public class ContextDeclarationsParsingTests
    {
        private const string ContextIdentifier = "context";
        private const string ContextName = "TestContext";
        private const string ContextType = "TestContextType";
        private const string ContextDefinitionIdentifier = "is";
        private const string Param = "$testParam";
        private const string Params = "$testParam1, $testParam2";
        private const string SingleCondition = "Name=\"TestName\"";
        private const string CompositeCondition = "Name=\"TestName\", ControlType=\"Button\"";

        private class ValidContexts
        {
            public const string NoParamsNoCondition    = ContextIdentifier + " " + ContextName + ":";
            public const string SingleParam            = ContextIdentifier + " " + ContextName + "(" + Param + "):";
            public const string MultipleParams         = ContextIdentifier + " " + ContextName + "(" + Params + "):";
            public const string SingleRootCondition    = ContextIdentifier + " " + ContextName + " " + ContextDefinitionIdentifier + " " + ContextType + "[" + SingleCondition + "]:";
            public const string CompositeRootCondition = ContextIdentifier + " " + ContextName + " " + ContextDefinitionIdentifier + " " + ContextType + "[" + CompositeCondition + "]:";
            public const string ParamsAndRootCondition = ContextIdentifier + " " + ContextName + "(" + Params + ")" + " " + ContextDefinitionIdentifier + " " + ContextType + "[" + CompositeCondition + "]:";
        }

        private class InvalidContexts
        {
            public const string MissingContextType         = ContextIdentifier + " " + ContextName + " " + ContextDefinitionIdentifier + " [" + SingleCondition + "]:";
            public const string MissingName                = ContextIdentifier + " :";
            public const string MultipleParamsDeclarations = ContextIdentifier + " " + ContextName + "(" + Params + ") (" + Params + "):";
            public const string MultipleRootConditions     = ContextIdentifier + " " + ContextName + " " + ContextDefinitionIdentifier + " " + ContextType + "[" + SingleCondition + "] [" + SingleCondition + "]:";
            public const string ParamsAfterRootCondition   = ContextIdentifier + " " + ContextName + " " + ContextDefinitionIdentifier + " " + ContextType + "[" + SingleCondition + "] (" + Params + "):";
            public const string TextAfterColon             = ContextIdentifier + " " + ContextName + ": SomeTestText";
        }


        [TestMethod]
        public void ContextNameIsParsed()
        {
            ScriptParser parser = new ScriptParser();
            ContextDefinition contextDefinition = parser.ParseContextDefinitionDeclaration(null, ValidContexts.SingleRootCondition);

            Assert.IsNotNull(contextDefinition, "The parsed IContextDefinition should not be null.");
            Assert.AreEqual(ContextName, contextDefinition.Name, "The parsed IContextDefinition's ContextName should be the given name.");
        }


        [DataRow(ValidContexts.NoParamsNoCondition,             DisplayName = "ValidContext_NoParamsNoCondition")]
        [DataRow(ValidContexts.SingleParam,                     DisplayName = "ValidContext_SingleParam")]
        [DataRow(ValidContexts.MultipleParams,                  DisplayName = "ValidContext_MultipleParams")]
        [DataRow(ValidContexts.SingleRootCondition,             DisplayName = "ValidContext_SingleRootCondition")]
        [DataRow(ValidContexts.CompositeRootCondition,          DisplayName = "ValidContext_CompositeRootCondition")]
        [DataRow(ValidContexts.ParamsAndRootCondition,          DisplayName = "ValidContext_ParamsAndRootCondition")]
        [DataTestMethod]
        public void ValidContextsCanBeParsed(string contextDeclaration)
        {
            ScriptParser parser = new ScriptParser();
            ContextDefinition contextDefinition = parser.ParseContextDefinitionDeclaration(null, contextDeclaration);

            Assert.IsNotNull(contextDefinition, "The parsed IContextDefinition should not be null.");
        }


        [DataRow(InvalidContexts.MissingContextType,         DisplayName = "InvalidContext_MissingContextType")]
        [DataRow(InvalidContexts.MissingName,                DisplayName = "InvalidContext_MissingName")]
        [DataRow(InvalidContexts.MultipleParamsDeclarations, DisplayName = "InvalidContext_MultipleParamsDeclarations")]
        [DataRow(InvalidContexts.MultipleRootConditions,     DisplayName = "InvalidContext_MultipleRootConditions")]
        [DataRow(InvalidContexts.ParamsAfterRootCondition,   DisplayName = "InvalidContext_ParamsAfterRootCondition")]
        [DataRow(InvalidContexts.TextAfterColon,             DisplayName = "InvalidContext_TextAfterColon")]
        [DataTestMethod]
        public void InvalidContextsThrowException(string contextDeclaration)
        {
            ScriptParser parser = new ScriptParser();
            Assert.ThrowsException<InvalidContextDeclarationException>(() => parser.ParseContextDefinitionDeclaration(null, contextDeclaration));
        }
    }
}
