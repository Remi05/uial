using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Contexts.Windows;
using Uial.Interactions;
using Uial.Interactions.Windows;
using Uial.UnitTests.Contexts;
using Uial.UnitTests.Contexts.Windows;

namespace Uial.UnitTests.Interactions.Windows
{
    [TestClass]
    public class VisualInteractionProviderTests
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
            var interactionProvider = new VisualInteractionProvider();

            // Act
            bool actualIsKnownInteraction = interactionProvider.IsKnownInteraction(interactionName);
            
            // Assert
            Assert.IsTrue(actualIsKnownInteraction);
        }

        [DataRow("TestUnknownInteraction")]
        [DataTestMethod]
        public void VerifyInteractionIsNotKnown(string interactionName)
        {
            // Arrange
            var interactionProvider = new VisualInteractionProvider();

            // Act
            bool actualIsKnownInteraction = interactionProvider.IsKnownInteraction(interactionName);

            // Assert
            Assert.IsFalse(actualIsKnownInteraction);
        }

        [DataRow("TestUnknownInteraction")]
        [DataTestMethod]
        public void VerifyGettingUnknownInteractionThrows(string interactionName)
        {
            // Arrange
            var interactionProvider = new VisualInteractionProvider();

            // Act + Assert
            Assert.ThrowsException<InteractionUnavailableException>(() => interactionProvider.GetInteractionByName(new MockWindowsVisualContext(), null, interactionName, null));
        }

        [TestMethod]
        public void VerifyGettingInteractionOutsideWindowsContextThrows()
        {
            // Arrange
            var interactionProvider = new VisualInteractionProvider();

            // Act + Assert
            Assert.ThrowsException<InvalidWindowsVisualContextException>(() => interactionProvider.GetInteractionByName(new MockContext(), null, null, null));
        }
    }
}
