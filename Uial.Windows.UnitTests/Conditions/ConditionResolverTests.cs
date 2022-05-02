using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomationClient;
using Uial.DataModels;
using Uial.Values;
using Uial.Windows.Conditions;
using Uial.UnitTests.Values;

using AutomationPropertyIdentifier = System.Int32;

namespace Uial.Windows.UnitTests.Conditions
{
    [TestClass]
    public class ConditionResolverTests
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
        public void VerifyResolvingNullConditionDefinitionThrows()
        {
            var conditionResolver = new ConditionResolver(null);
            Assert.ThrowsException<ArgumentNullException>(() => conditionResolver.Resolve(null, new ReferenceValueStore()));
        }

        [TestMethod]
        public void VerifyControlTypeConditionIsResolved()
        {
            AutomationPropertyIdentifier expectedProperty = UIA_PropertyIds.UIA_ControlTypePropertyId;
            var expectedControlType = UIA_ControlTypeIds.UIA_ButtonControlTypeId;
            var valueDefinition = new LiteralValueDefinition("Button");

            var propertyConditionDefinition = new PropertyConditionDefinition(Properties.GetPropertyUialString(expectedProperty), valueDefinition);
            var conditionResolver = new ConditionResolver(new MockValueResolver());
            var actualCondition = conditionResolver.Resolve(propertyConditionDefinition, null) as IUIAutomationPropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.AreEqual(expectedProperty, actualCondition.propertyId);
            Assert.AreEqual(expectedControlType, actualCondition.PropertyValue);
        }

        [TestMethod]
        public void VerifyNameConditionIsResolved()
        {
            AutomationPropertyIdentifier expectedProperty = UIA_PropertyIds.UIA_NamePropertyId;
            string expectedName = "TestControlName";
            var valueDefinition = new LiteralValueDefinition(expectedName);

            var propertyConditionDefinition = new PropertyConditionDefinition(Properties.GetPropertyUialString(expectedProperty), valueDefinition);
            var conditionResolver = new ConditionResolver(new MockValueResolver());
            var actualCondition = conditionResolver.Resolve(propertyConditionDefinition, null) as IUIAutomationPropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.AreEqual(expectedProperty, actualCondition.propertyId);
            Assert.AreEqual(expectedName, actualCondition.PropertyValue as string);
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

            var conditionDefinitions = new List<PropertyConditionDefinition>();
            foreach (var condition in expectedConditions)
            {
                var conditionDefinition = new PropertyConditionDefinition(Properties.GetPropertyUialString(condition.propertyId), new LiteralValueDefinition(condition.PropertyValue as string));
                conditionDefinitions.Add(conditionDefinition);
            }

            var compositeConditionDefinition = new CompositeConditionDefinition(conditionDefinitions);
            var conditionResolver = new ConditionResolver(new MockValueResolver());
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
            var conditionResolver = new ConditionResolver(new MockValueResolver());
            var actualCondition = conditionResolver.Resolve(compositeConditionDefinition, null) as IUIAutomationPropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.IsTrue(new ProperyConditionComparer().Equals(expectedCondition, actualCondition));
        }
    }
}
