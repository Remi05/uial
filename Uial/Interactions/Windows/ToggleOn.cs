using UIAutomationClient;
using Uial.Contexts.Windows;

using AutomationPatternIdentifier = System.Int32;

namespace Uial.Interactions.Windows;

internal class ToggleOn : AbstractPatternInteraction<IUIAutomationTogglePattern>, IInteraction
{
    public const string Key = "ToggleOn";

    public override string Name => Key;
    protected override AutomationPatternIdentifier AutomationPattern => UIA_PatternIds.UIA_TogglePatternId;

    public ToggleOn(IWindowsVisualContext context) : base(context) { }

    public override void Do()
    {
        base.Do();
        if (Pattern.CurrentToggleState == ToggleState.ToggleState_Off)
        { 
            Pattern.Toggle();
        }
    }
}
