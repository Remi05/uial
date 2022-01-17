using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomationClient;
using Uial.DataModels;
using Uial.Windows.Conditions;

using AutomationPropertyIdentifier = System.Int32;

namespace Uial.UnitTests.Conditions
{
    [TestClass]
    public class PropertyConditionResolverTests
    {
        [TestMethod]
        public void VerifyControlTypeConditionIsResolved()
        {
            AutomationPropertyIdentifier expectedProperty = UIA_PropertyIds.UIA_ControlTypePropertyId;
            var expectedControlType = UIA_ControlTypeIds.UIA_ButtonControlTypeId;
            var valueDefinition = new LiteralValueDefinition("Button");

            var propertyConditionDefinition = new PropertyConditionDefinition(Properties.GetControlTypeUialString(expectedProperty), valueDefinition);
            var conditionResolver = new ConditionResolver(null);
            var actualCondition = conditionResolver.Resolve(propertyConditionDefinition, null) as IUIAutomationPropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.AreEqual(expectedProperty, actualCondition.propertyId);
            Assert.AreEqual(expectedControlType, actualCondition.PropertyValue);
        }

        [TestMethod]
        public void VerifyConditionIsResolved()
        {
            AutomationPropertyIdentifier expectedProperty = UIA_PropertyIds.UIA_NamePropertyId;
            string expectedName = "TestControlName";
            var valueDefinition = new LiteralValueDefinition(expectedName);

            var propertyConditionDefinition = new PropertyConditionDefinition(Properties.GetPropertyUialString(expectedProperty), valueDefinition);
            var conditionResolver = new ConditionResolver(null);
            var actualCondition = conditionResolver.Resolve(propertyConditionDefinition, null) as IUIAutomationPropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.AreEqual(expectedProperty, actualCondition.propertyId);
            Assert.AreEqual(expectedName, actualCondition.PropertyValue as string);
        }
    }
}
