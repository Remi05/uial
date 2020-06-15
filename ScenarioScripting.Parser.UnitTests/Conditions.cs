﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScenarioScripting.Conditions;
using ScenarioScripting.Parser.Exceptions;

namespace ScenarioScripting.Parser.UnitTests
{
    [TestClass]
    public class ConditionsParsingTests
    {
        private class ValidConditions
        {
            public const string SingleLitteralCondition     = "Name=\"TestButton\"";
            public const string CompositeLitteralCondition  = "Name=\"TestButton\", ControlType=\"Button\"";
            public const string SingleReferenceCondition    = "Name=$testName";
            public const string CompositeReferenceCondition = "Name=$testName, AutomationId=$testId";
            public const string CompositeMixedCondition     = "Name=$testName, ControlType=\"Button\"";
        }

        private class InvalidConditions
        {
            public const string EmptyCondition = "";
        }


        [DataRow(ValidConditions.SingleLitteralCondition,     DisplayName = "ValidConditions_SingleLitteralCondition")]
        [DataRow(ValidConditions.CompositeLitteralCondition , DisplayName = "ValidConditions_CompositeLitteralCondition")]
        [DataRow(ValidConditions.SingleReferenceCondition,    DisplayName = "ValidConditions_SingleReferenceCondition")]
        [DataRow(ValidConditions.CompositeReferenceCondition, DisplayName = "ValidConditions_CompositeReferenceCondition")]
        [DataRow(ValidConditions.CompositeMixedCondition,     DisplayName = "ValidConditions_CompositeMixedCondition")]
        [DataTestMethod]
        public void ValidConditionsCanBeParsed(string conditionStr)
        {
            ScriptParser parser = new ScriptParser();
            IConditionDefinition conditionDefinition = parser.ParseConditionDefinition(conditionStr);

            Assert.IsNotNull(conditionDefinition, "The parsed IBaseContextDefinition should not be null.");
        }


        [DataRow(InvalidConditions.EmptyCondition, DisplayName = "InvalidConditions_EmptyCondition")]
        [DataTestMethod]
        public void InvalidConditionsThrowException(string conditionStr)
        {
            ScriptParser parser = new ScriptParser();
            Assert.ThrowsException<InvalidConditionException>(() => parser.ParseConditionDefinition(conditionStr));
        }
    }
}
