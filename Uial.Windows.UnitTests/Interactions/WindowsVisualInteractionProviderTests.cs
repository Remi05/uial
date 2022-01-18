using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Windows.Contexts;
using Uial.Interactions;
using Uial.Windows.Interactions;
using Uial.UnitTests.Contexts; // For MockContext, TODO: Review
using Uial.UnitTests.Windows.Contexts;

namespace Uial.UnitTests.Windows.Interactions
{
    [TestClass]
    public class WindowsVisualInteractionProviderTests
    {
        [DataRow(Close.Key)]
        [DataRow(Collapse.Key)]
        [DataRow(Expand.Key)]
        [DataRow(Focus.Key)]
        [DataRow(GetPropertyValue.Key)]
        [DataRow(GetRangeValue.Key)]
        [DataRow(GetTextValue.Key)]
        [DataRow(Invoke.Key)]
        [DataRow(Maximize.Key)]
        [DataRow(Minimize.Key)]
        [DataRow(Move.Key)]
        [DataRow(Resize.Key)]
        [DataRow(Restore.Key)]
        [DataRow(Scroll.Key)]
        [DataRow(Select.Key)]
        [DataRow(SetRangeValue.Key)]
        [DataRow(SetTextValue.Key)]
        [DataRow(Toggle.Key)]
        [DataTestMethod]
        public void VerifyInteractionIsKnown(string interactionName)
        {
            // Arrange
            var interactionProvider = new WindowsVisualInteractionProvider();

            // Act
            bool actualIsKnownInteraction = interactionProvider.IsInteractionAvailableForContext(interactionName, null);
            
            // Assert
            Assert.IsTrue(actualIsKnownInteraction);
        }

        [DataRow("TestUnknownInteraction")]
        [DataTestMethod]
        public void VerifyInteractionIsNotKnown(string interactionName)
        {
            // Arrange
            var interactionProvider = new WindowsVisualInteractionProvider();

            // Act
            bool actualIsKnownInteraction = interactionProvider.IsInteractionAvailableForContext(interactionName, null);

            // Assert
            Assert.IsFalse(actualIsKnownInteraction);
        }

        [DataRow("TestUnknownInteraction")]
        [DataTestMethod]
        public void VerifyGettingUnknownInteractionThrows(string interactionName)
        {
            // Arrange
            var interactionProvider = new WindowsVisualInteractionProvider();

            // Act + Assert
            Assert.ThrowsException<InteractionUnavailableException>(() => interactionProvider.GetInteractionByName(interactionName, null, new MockWindowsVisualContext()));
        }

        [TestMethod]
        public void VerifyGettingInteractionOutsideWindowsContextThrows()
        {
            // Arrange
            var interactionProvider = new WindowsVisualInteractionProvider();

            // Act + Assert
            Assert.ThrowsException<InvalidWindowsVisualContextException>(() => interactionProvider.GetInteractionByName(null, null, new MockContext()));
        }
    }
}
