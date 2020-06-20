using System.Windows.Automation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Conditions;

namespace Uial.UnitTests.Conditions
{
    [TestClass]
    public class PropertyConditionDefinitionTests
    {
        [TestMethod]
        public void VerifyControlTypeConditionIsResolved()
        {
            AutomationProperty expectedProperty = AutomationElement.ControlTypeProperty;
            ControlType expectedControlType = ControlType.Button;
            var valueDefinition = ValueDefinition.FromLiteral("Button");

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
            var valueDefinition = ValueDefinition.FromLiteral(expectedName);

            var propertyConditionDefinition = new PropertyConditionDefinition(expectedProperty, valueDefinition);
            var actualCondition = propertyConditionDefinition.Resolve(null) as PropertyCondition;

            Assert.IsNotNull(actualCondition);
            Assert.AreEqual(expectedProperty, actualCondition.Property);
            Assert.AreEqual(expectedName, actualCondition.Value as string);
        }
    }
}
