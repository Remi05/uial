using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Interactions;
using Uial.Interactions.Processes;

namespace Uial.UnitTests.Interactions.Processes;

[TestClass]
public class ProcessInteractionProviderTests
{
    [DataRow(StartProcess.Key)]
    [DataTestMethod]
    public void VerifyInteractionIsKnown(string interactionName)
    {
        // Arrange
        var interactionProvider = new ProcessInteractionProvider();

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
        var interactionProvider = new ProcessInteractionProvider();

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
        var interactionProvider = new ProcessInteractionProvider();

        // Act + Assert
        Assert.ThrowsException<InteractionUnavailableException>(() => interactionProvider.GetInteractionByName(null, null, interactionName, null));
    }
}
