using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using Uial.Conditions;

namespace Uial.Recorder
{
    class Program
    {
        static void Main(string[] args)
        {
            ScriptRecorder recorder = new ScriptRecorder();
            var foundElements = new List<AutomationElement>();

            AutomationElement prevElement = null;
            while (true)
            {
                AutomationElement curElement = recorder.GetCurrentElment();
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
