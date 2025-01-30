using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows;

internal class ToggleOff : AbstractPatternInteraction<IUIAutomationTogglePattern>, IInteraction
{
    public const string Key = "ToggleOff";

    public override string Name => Key;
    protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_TogglePatternId;

    public ToggleOff(IWindowsVisualContext context) : base(context) { }

    public override void Do()
    {
        base.Do();
        if (Pattern.CurrentToggleState == ToggleState.ToggleState_On)
        { 
            Pattern.Toggle();
        }
    }
}
