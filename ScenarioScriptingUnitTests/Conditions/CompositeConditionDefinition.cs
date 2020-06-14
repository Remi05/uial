using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScenarioScripting;
using ScenarioScripting.Conditions;

namespace ScenarioScriptingUnitTests.Conditions
{
    [TestClass]
    public class CompositeConditionDefinitionTests
    {
        private class ProperyConditionComparer : IEqualityComparer<PropertyCondition>
        {
            public bool Equals(PropertyCondition first, PropertyCondition second)
            {
                return first.Property == first.Property && first.Value == first.Value;
            }

            public int GetHashCode(PropertyCondition propertyCondition)
            {
                return propertyCondition.Property.GetHashCode() ^ propertyCondition.Value.GetHashCode();
            }
        }

        private void FlattenConditions(AndCondition andCondition, List<PropertyCondition> conditions)
        {
            foreach (Condition condition in andCondition.GetConditions())
            {
                if (condition is AndCondition)
                {
                    FlattenConditions(condition as AndCondition, conditions);
                }
                else
                {
                    conditions.Add(condition as PropertyCondition);
                }
            }
        }

        [TestMethod]
        public void VerifyCompositeConditionIsResolved()
        {
            var expectedConditions = new List<PropertyCondition>()
            {
                new PropertyCondition(AutomationElement.AutomationIdProperty, "TestAutomationId"),
                new PropertyCondition(AutomationElement.ClassNameProperty, "TestClassControlName"),
                new PropertyCondition(AutomationElement.NameProperty, "TestControlName"),
            };

            IEnumerable<PropertyConditionDefinition> conditionDefinitions = expectedConditions.Select(
                (condition) => new PropertyConditionDefinition(condition.Property, ValueDefinition.FromLitteral(condition.Value as string))
            );

            var compositeConditionDefinition = new CompositeConditionDefinition(conditionDefinitions);
            var compositeConditon = compositeConditionDefinition.Resolve(null) as AndCondition;
            Assert.IsNotNull(compositeConditon);

            var actualConditions = new List<PropertyCondition>();
            FlattenConditions(compositeConditon, actualConditions);

            Assert.IsTrue(expectedConditions.SequenceEqual(actualConditions, new ProperyConditionComparer()));
        }

        [TestMethod]
        public void VerifySingleConditionIsResolved()
        {
            var expectedCondition = new PropertyCondition(AutomationElement.NameProperty, "TestControlName");
            var conditionDefinition = new PropertyConditionDefinition(expectedCondition.Property, ValueDefinition.FromLitteral(expectedCondition.Value as string));
            var conditionDefinitions = new List<IConditionDefinition>() { conditionDefinition };

            var compositeConditionDefinition = new CompositeConditionDefinition(conditionDefinitions);
            var actualCondition = compositeConditionDefinition.Resolve(null) as PropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.IsTrue(new ProperyConditionComparer().Equals(expectedCondition, actualCondition));
        }
    }
}
