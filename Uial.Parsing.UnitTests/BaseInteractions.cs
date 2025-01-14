using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.DataModels;
using Uial.Parsing.Exceptions;

namespace Uial.Parsing.UnitTests
{
    [TestClass]
    public class BaseInteractionsParsingTests
    {
        private class ValidBaseInteractions
        {
            public const string CoreInteractionControl         = "Button[Name=\"TestButton\"]::Invoke()";
            public const string CoreInteractionNamedContext    = "TestContext::Invoke()";
            public const string CoreInteractionNoContext       = "::Invoke()";
            public const string CoreInteractionSingleParam     = "::SetValue($testParam)";
            public const string CoreInteractionMultipleParams  = "::Scroll($testParam1, $testParam2)";
            public const string NamedInteractionControl        = "Menu[Name=\"TestMenu\"]::TestInteraction()";
            public const string NamedInteractionNamedContext   = "TestContext::TestInteraction()";
            public const string NamedInteractionNoContext      = "::TestInteraction()";
            public const string NamedInteractionSingleParam    = "::TestInteraction($testParam)";
            public const string NamedInteractionMultipleParams = "::TestInteraction($testParam1, $testParam2)";
        } 

        private class InvalidBaseInteraction
        {
            public const string CoreInteractionMissingColons  = "Invoke()";
            public const string NamedInteractionMissingColons = "TestInteraction()";
            public const string MissingParantheses = "::Invoke";
            public const string TextAfterParams = "::Invoke() SomeText";
        }


        [DataRow(ValidBaseInteractions.CoreInteractionControl,         DisplayName = "ValidBaseInteractions_CoreInteractionControl")]
        [DataRow(ValidBaseInteractions.CoreInteractionNamedContext,    DisplayName = "ValidBaseInteractions_CoreInteractionNamedContext")]
        [DataRow(ValidBaseInteractions.CoreInteractionNoContext,       DisplayName = "ValidBaseInteractions_CoreInteractionNoContext")]
        [DataRow(ValidBaseInteractions.CoreInteractionSingleParam,     DisplayName = "ValidBaseInteractions_CoreInteractionSingleParam")]
        [DataRow(ValidBaseInteractions.CoreInteractionMultipleParams,  DisplayName = "ValidBaseInteractions_CoreInteractionMultipleParams")]
        [DataRow(ValidBaseInteractions.NamedInteractionControl,        DisplayName = "ValidBaseInteractions_NamedInteractionControl")]
        [DataRow(ValidBaseInteractions.NamedInteractionNamedContext,   DisplayName = "ValidBaseInteractions_NamedInteractionNamedContext")]
        [DataRow(ValidBaseInteractions.NamedInteractionNoContext,      DisplayName = "ValidBaseInteractions_NamedInteractionNoContext")]
        [DataRow(ValidBaseInteractions.NamedInteractionSingleParam,    DisplayName = "ValidBaseInteractions_NamedInteractionSingleParam")]
        [DataRow(ValidBaseInteractions.NamedInteractionMultipleParams, DisplayName = "ValidBaseInteractions_NamedInteractionMultipleParams")]
        [DataTestMethod]
        public void ValidBaseInteractionsCanBeParsed(string baseInteractionStr)
        {
            ScriptParser parser = new ScriptParser();
            BaseInteractionDefinition baseInteraction = parser.ParseBaseInteractionDefinition(baseInteractionStr);

            Assert.IsNotNull(baseInteraction, "The parsed IBaseInteractionDefinition should not be null.");
        }


        [DataRow(InvalidBaseInteraction.CoreInteractionMissingColons,  DisplayName = "InvalidBaseInteraction_CoreInteractionMissingColons")]
        [DataRow(InvalidBaseInteraction.NamedInteractionMissingColons, DisplayName = "InvalidBaseInteraction_NamedInteractionMissingColons")]
        [DataRow(InvalidBaseInteraction.MissingParantheses, DisplayName = "InvalidBaseInteraction_MissingParantheses")]
        [DataRow(InvalidBaseInteraction.TextAfterParams, DisplayName = "InvalidBaseInteraction_TextAfterParams")]
        [DataTestMethod]
        public void InvalidBaseInteractionsThrowException(string baseInteractionStr)
        {
            ScriptParser parser = new ScriptParser();
            Assert.ThrowsException<InvalidBaseInteractionException>(() => parser.ParseBaseInteractionDefinition(baseInteractionStr));
        }
    }
}
