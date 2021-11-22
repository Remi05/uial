using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomationClient;
using Uial.Conditions;

namespace Uial.UnitTests.Conditions
{
    [TestClass]
    public class CompositeConditionDefinitionTests
    {
        private class ProperyConditionComparer : IEqualityComparer<IUIAutomationPropertyCondition>
        {
            public bool Equals(IUIAutomationPropertyCondition first, IUIAutomationPropertyCondition second)
            {
                return first.propertyId.Equals(second.propertyId) && first.PropertyValue.Equals(second.PropertyValue);
            }

            public int GetHashCode(IUIAutomationPropertyCondition propertyCondition)
            {
                return propertyCondition.propertyId.GetHashCode() ^ propertyCondition.PropertyValue.GetHashCode();
            }
        }

        private void FlattenConditions(IUIAutomationAndCondition andCondition, List<IUIAutomationPropertyCondition> conditions)
        {
            foreach (IUIAutomationCondition condition in andCondition.GetChildren())
            {
                if (condition is IUIAutomationAndCondition)
                {
                    FlattenConditions(condition as IUIAutomationAndCondition, conditions);
                }
                else
                {
                    conditions.Add(condition as IUIAutomationPropertyCondition);
                }
            }
        }

        [TestMethod]
        public void VerifyCompositeConditionIsResolved()
        {
            var uiAutomation = new CUIAutomation();
            var expectedConditions = new List<IUIAutomationPropertyCondition>()
            {
                uiAutomation.CreatePropertyCondition(UIA_PropertyIds.UIA_AutomationIdPropertyId, "TestAutomationId") as IUIAutomationPropertyCondition,
                uiAutomation.CreatePropertyCondition(UIA_PropertyIds.UIA_ClassNamePropertyId, "TestClassName") as IUIAutomationPropertyCondition,
                uiAutomation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "TestName") as IUIAutomationPropertyCondition,
            };

            IEnumerable<PropertyConditionDefinition> conditionDefinitions = expectedConditions.Select(
                (condition) => new PropertyConditionDefinition(condition.propertyId, ValueDefinition.FromLiteral(condition.PropertyValue as string))
            );

            var compositeConditionDefinition = new CompositeConditionDefinition(conditionDefinitions);
            var compositeConditon = compositeConditionDefinition.Resolve(null) as IUIAutomationAndCondition;
            Assert.IsNotNull(compositeConditon);

            var actualConditions = new List<IUIAutomationPropertyCondition>();
            FlattenConditions(compositeConditon, actualConditions);

            Assert.IsTrue(expectedConditions.SequenceEqual(actualConditions, new ProperyConditionComparer()));
        }

        [TestMethod]
        public void VerifySingleConditionIsResolved()
        {
            var uiAutomation = new CUIAutomation();
            var expectedCondition = uiAutomation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "TestControlName") as IUIAutomationPropertyCondition;
            var conditionDefinition = new PropertyConditionDefinition(expectedCondition.propertyId, ValueDefinition.FromLiteral(expectedCondition.PropertyValue as string));
            var conditionDefinitions = new List<IConditionDefinition>() { conditionDefinition };

            var compositeConditionDefinition = new CompositeConditionDefinition(conditionDefinitions);
            var actualCondition = compositeConditionDefinition.Resolve(null) as IUIAutomationPropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.IsTrue(new ProperyConditionComparer().Equals(expectedCondition, actualCondition));
        }
    }
}
