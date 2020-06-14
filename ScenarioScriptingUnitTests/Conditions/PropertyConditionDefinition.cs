using System.Windows.Automation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScenarioScripting;
using ScenarioScripting.Conditions;

namespace ScenarioScriptingUnitTests.Conditions
{
    [TestClass]
    public class PropertyConditionDefinitionTests
    {
        [TestMethod]
        public void VerifyControlTypeConditionIsResolved()
        {
            AutomationProperty expectedProperty = AutomationElement.ControlTypeProperty;
            ControlType expectedControlType = ControlType.Button;
            ValueDefinition valueDefinition = ValueDefinition.FromLitteral("Button");

            PropertyConditionDefinition propertyConditionDefinition = new PropertyConditionDefinition(expectedProperty, valueDefinition);
            PropertyCondition actualCondition = propertyConditionDefinition.Resolve(null) as PropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.AreEqual(expectedProperty, actualCondition.Property);
            Assert.AreEqual(expectedControlType.Id, actualCondition.Value);
        }

        [TestMethod]
        public void VerifyConditionIsResolved()
        {
            AutomationProperty expectedProperty = AutomationElement.NameProperty;
            string expectedName = "TestControlName";
            ValueDefinition valueDefinition = ValueDefinition.FromLitteral(expectedName);

            PropertyConditionDefinition propertyConditionDefinition = new PropertyConditionDefinition(expectedProperty, valueDefinition);
            PropertyCondition actualCondition = propertyConditionDefinition.Resolve(null) as PropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.AreEqual(expectedProperty, actualCondition.Property);
            Assert.AreEqual(expectedName, actualCondition.Value as string);
        }
    }
}
