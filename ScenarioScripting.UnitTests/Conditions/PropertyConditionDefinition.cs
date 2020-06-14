using System.Windows.Automation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScenarioScripting.Conditions;

namespace ScenarioScripting.UnitTests.Conditions
{
    [TestClass]
    public class PropertyConditionDefinitionTests
    {
        [TestMethod]
        public void VerifyControlTypeConditionIsResolved()
        {
            AutomationProperty expectedProperty = AutomationElement.ControlTypeProperty;
            ControlType expectedControlType = ControlType.Button;
            var valueDefinition = ValueDefinition.FromLitteral("Button");

            var propertyConditionDefinition = new PropertyConditionDefinition(expectedProperty, valueDefinition);
            var actualCondition = propertyConditionDefinition.Resolve(null) as PropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.AreEqual(expectedProperty, actualCondition.Property);
            Assert.AreEqual(expectedControlType.Id, actualCondition.Value);
        }

        [TestMethod]
        public void VerifyConditionIsResolved()
        {
            AutomationProperty expectedProperty = AutomationElement.NameProperty;
            string expectedName = "TestControlName";
            var valueDefinition = ValueDefinition.FromLitteral(expectedName);

            var propertyConditionDefinition = new PropertyConditionDefinition(expectedProperty, valueDefinition);
            var actualCondition = propertyConditionDefinition.Resolve(null) as PropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.AreEqual(expectedProperty, actualCondition.Property);
            Assert.AreEqual(expectedName, actualCondition.Value as string);
        }
    }
}
