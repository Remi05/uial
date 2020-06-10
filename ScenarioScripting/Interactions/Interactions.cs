using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace ScenarioScripting.Interactions
{
    public class Interactions
    {
        //public static readonly Dictionary<string, AutomationPattern> PatternMap = new Dictionary<string, AutomationPattern>()
        //{
        //    { CloseWindow.Key, CloseWindow.Pattern },
        //    { Invoke.Key,      Invoke.Pattern },
        //    { Scroll.Key,      Scroll.Pattern },
        //    { Select.Key,      Select.Pattern },
        //    { SetValue.Key,    SetValue.Pattern },
        //    { Toggle.Key,      Toggle.Pattern },
        //};

        public static readonly Dictionary<string, IInteraction> BaseInteractions = new Dictionary<string, IInteraction>()
        {
        };

        public static IInteraction GetBasicInteractionByName(string interactionName)
        {
            return null;
        }
    }
}
