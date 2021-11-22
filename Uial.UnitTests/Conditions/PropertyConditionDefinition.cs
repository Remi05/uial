using Microsoft.VisualStudio.TestTools.UnitTesting;
using UIAutomationClient;
using Uial.Conditions;

using AutomationPropertyIdentifier = System.Int32;

namespace Uial.UnitTests.Conditions
{
    [TestClass]
    public class PropertyConditionDefinitionTests
    {
        [TestMethod]
        public void VerifyControlTypeConditionIsResolved()
        {
            AutomationPropertyIdentifier expectedProperty = UIA_PropertyIds.UIA_ControlTypePropertyId;
            var expectedControlType = UIA_ControlTypeIds.UIA_ButtonControlTypeId;
            var valueDefinition = ValueDefinition.FromLiteral("Button");

            var propertyConditionDefinition = new PropertyConditionDefinition(expectedProperty, valueDefinition);
            var actualCondition = propertyConditionDefinition.Resolve(null) as IUIAutomationPropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.AreEqual(expectedProperty, actualCondition.propertyId);
            Assert.AreEqual(expectedControlType, actualCondition.PropertyValue);
        }

        [TestMethod]
        public void VerifyConditionIsResolved()
        {
            AutomationPropertyIdentifier expectedProperty = UIA_PropertyIds.UIA_NamePropertyId;
            string expectedName = "TestControlName";
            var valueDefinition = ValueDefinition.FromLiteral(expectedName);

            var propertyConditionDefinition = new PropertyConditionDefinition(expectedProperty, valueDefinition);
            var actualCondition = propertyConditionDefinition.Resolve(null) as IUIAutomationPropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.AreEqual(expectedProperty, actualCondition.propertyId);
            Assert.AreEqual(expectedName, actualCondition.PropertyValue as string);
        }
    }
}
