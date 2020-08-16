using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace Uial.Recorder
{
    class Program
    {
        static void Main(string[] args)
        {
            var recorder = new ScriptRecorder();
            var foundElements = new List<AutomationElement>();

            AutomationElement prevElement = null;
            while (true)
            {
                AutomationElement curElement = recorder.GetCurrentElement();
                if (prevElement == null || !Automation.Compare(curElement, prevElement))
                {
                    prevElement = curElement;
                    foundElements.Add(curElement);
                    recorder.RegisterEventHandlers(curElement);
                    Console.WriteLine($"[{Conditions.Conditions.GetConditionFromElement(curElement)}]\n");
                }
            }
        }
    }
}
