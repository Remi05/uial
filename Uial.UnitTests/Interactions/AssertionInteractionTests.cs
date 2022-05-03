using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Interactions;
using Uial.UnitTests.Assertions;

namespace Uial.UnitTests.Interactions
{
    [TestClass]
    public class AssertionInteractionTests
    {
        [TestMethod]
        public void VerifyNameIsSet()
        {
            string assertionName = "MockAssertion";
            string expectedInteractionName = "Assert::" + assertionName;

            var mockAssertion = new MockAssertion(assertionName, () => true);
            var assertionInteraction = new AssertionInteraction(mockAssertion);

            Assert.AreEqual(expectedInteractionName, assertionInteraction.Name, "AssertionInteraction's Name property should be equal to its Assertion's Name prefixed by \"Assert::\".");
        }

        [TestMethod]
        public void VerifyAssertionIsRunOnlyOnce()
        {
            var mockAssertion = new MockAssertion("MockAssertion", () => true);
            var assertionInteraction = new AssertionInteraction(mockAssertion);
            assertionInteraction.Do();

            Assert.IsTrue(mockAssertion.WasRun, "The assertion should be run when AssertionInteraction's Do() method is called.");
            Assert.IsTrue(mockAssertion.WasRunOnce, "The assertion should be run exactly once when AssertionInteraction's Do() method is called.");
        }

        [TestMethod]
        public void VerifySuccessfulAssertionDoesntThrow()
        {
            var mockAssertion = new MockAssertion("SuccessfulAssertion", () => true);
            var assertionInteraction = new AssertionInteraction(mockAssertion);
            assertionInteraction.Do();
        }

        [TestMethod]
        public void VerifyFailedAssertionThrows()
        {
            var mockAssertion = new MockAssertion("FailedAssertion", () => false);
            var assertionInteraction = new AssertionInteraction(mockAssertion);
            Assert.ThrowsException<AssertionFailedException>(() => assertionInteraction.Do(), "AssertionInteraction's Do() method should throw an AssertionFailedException when the assertion fails.");
        }
    }
}
