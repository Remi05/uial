using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomationClient;
using Uial.DataModels;
using Uial.Windows.Conditions;

namespace Uial.Windows.UnitTests.Conditions
{
    [TestClass]
    public class CompositeConditionResolverTests
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
                (condition) => new PropertyConditionDefinition(Properties.GetPropertyUialString(condition.propertyId), new LiteralValueDefinition(condition.PropertyValue as string))
            );

            var compositeConditionDefinition = new CompositeConditionDefinition(conditionDefinitions);
            var conditionResolver = new ConditionResolver(null);
            var compositeCondition = conditionResolver.Resolve(compositeConditionDefinition, null);
            Assert.IsNotNull(compositeCondition);

            var actualConditions = new List<IUIAutomationPropertyCondition>();
            FlattenConditions(compositeCondition as IUIAutomationAndCondition, actualConditions);

            Assert.IsTrue(expectedConditions.SequenceEqual(actualConditions, new ProperyConditionComparer()));
        }

        [TestMethod]
        public void VerifySingleConditionIsResolved()
        {
            var uiAutomation = new CUIAutomation();
            var expectedCondition = uiAutomation.CreatePropertyCondition(UIA_PropertyIds.UIA_NamePropertyId, "TestControlName") as IUIAutomationPropertyCondition;
            var conditionDefinition = new PropertyConditionDefinition(Properties.GetPropertyUialString(expectedCondition.propertyId), new LiteralValueDefinition(expectedCondition.PropertyValue as string));
            var conditionDefinitions = new List<ConditionDefinition>() { conditionDefinition };

            var compositeConditionDefinition = new CompositeConditionDefinition(conditionDefinitions);
            var conditionResolver = new ConditionResolver(null);
            var actualCondition = conditionResolver.Resolve(compositeConditionDefinition, null) as IUIAutomationPropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.IsTrue(new ProperyConditionComparer().Equals(expectedCondition, actualCondition));
        }
    }
}
