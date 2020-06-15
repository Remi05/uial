using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScenarioScripting.Contexts;

namespace ScenarioScripting.Parser.UnitTests
{
    [TestClass]
    public class BaseContextsParsingTests
    {
        private class ValidBaseContexts
        {
            public const string SingleControl = "Button[Name=\"TestButton\"]";
            public const string MultipleControls = "Pane[Name=\"TestPane\"]::Button[Name=\"TestButton\"]";
            public const string SingleNamedContext = "TestNamedContext";
            public const string MultipleNamedContexts = "FirstNameContext::SecondNamedContext::ThirdNamedContext";
            public const string NamedContextAndControl = "TestNamedContext::Button[Name=\"TestButton\"]";
            public const string ControlAndNamedContext = "Button[Name=\"TestButton\"]::TestNamedContext";
        } 

        [DataRow(ValidBaseContexts.SingleControl,          DisplayName = "ValidBaseContext_SingleControl")]
        [DataRow(ValidBaseContexts.MultipleControls,       DisplayName = "ValidBaseContext_MultipleControls")]
        [DataRow(ValidBaseContexts.SingleNamedContext,     DisplayName = "ValidBaseContext_SingleNamedContext")]
        [DataRow(ValidBaseContexts.MultipleNamedContexts,  DisplayName = "ValidBaseContext_MultipleNamedContexts")]
        [DataRow(ValidBaseContexts.NamedContextAndControl, DisplayName = "ValidBaseContext_NamedContextAndControl")]
        [DataRow(ValidBaseContexts.ControlAndNamedContext, DisplayName = "ValidBaseContext_ControlAndNamedContext")]
        [DataTestMethod]
        public void ValidBaseContextsCanBeParsed(string baseContextStr)
        {
            ScriptParser parser = new ScriptParser();
            IBaseContextDefinition baseContextDefinition = parser.ParseBaseContextDefinition(baseContextStr);

            Assert.IsNotNull(baseContextDefinition, "The parsed IBaseContextDefinition should not be null.");
        }
    }
}
