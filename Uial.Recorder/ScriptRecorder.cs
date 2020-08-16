using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;
using Uial.Conditions;
using Uial.Contexts;
using Uial.Interactions;
using Uial.Interactions.Core;
using Uial.Scenarios;

namespace Uial.Recorder
{
    public class ScriptRecorder
    {
        private static readonly IList<AutomationProperty> IdentifyingProperties = new List<AutomationProperty>()
        {
            AutomationElement.AutomationIdProperty,
            AutomationElement.ClassNameProperty,
            AutomationElement.ControlTypeProperty,
            AutomationElement.NameProperty,
        };

        private static readonly List<AutomationEvent> AutomationEvents = new List<AutomationEvent>()
        {
            AutomationElement.MenuClosedEvent,
            AutomationElement.MenuOpenedEvent,
            InvokePattern.InvokedEvent,
            SelectionItemPattern.ElementAddedToSelectionEvent,
            SelectionItemPattern.ElementRemovedFromSelectionEvent,
            SelectionItemPattern.ElementSelectedEvent,
        };

        public ScriptRecorder()
        {
            Automation.AddAutomationFocusChangedEventHandler(OnFocusChanged);
        }

        ~ScriptRecorder()
        {
            Automation.RemoveAutomationFocusChangedEventHandler(OnFocusChanged);
        }

        public AutomationElement GetCurrentElement()
        {
            var point = new Point(Cursor.Position.X, Cursor.Position.Y);
            return AutomationElement.FromPoint(point);
        }

        public IConditionDefinition GetCurrentCondition()
        {
            AutomationElement currentElement = GetCurrentElement();
            return Conditions.Conditions.GetConditionFromElement(currentElement);
        }

        public IBaseContextDefinition GetCurrentBaseContext()
        {
            AutomationElement element = GetCurrentElement();
            ControlType controlType = element.GetCurrentPropertyValue(AutomationElement.ControlTypeProperty) as ControlType;

            List<IConditionDefinition> propertyConditions = new List<IConditionDefinition>();
            foreach (AutomationProperty property in IdentifyingProperties)
            {
                if (property == AutomationElement.ControlTypeProperty)
                {
                    continue;
                }

                object propertyValue = element.GetCurrentPropertyValue(property);
                if (propertyValue != null)
                {
                    ValueDefinition valueDefinition = ValueDefinition.FromLiteral(propertyValue.ToString());
                    PropertyConditionDefinition propertyCondition = new PropertyConditionDefinition(property, valueDefinition);
                    propertyConditions.Add(propertyCondition);
                }
            }

            IConditionDefinition identifyingCondition = new CompositeConditionDefinition(propertyConditions);
            return new BaseControlDefinition(controlType.ToUialString(), identifyingCondition);
        }

        public void RegisterEventHandlers(AutomationElement element)
        {
            foreach (AutomationEvent automationEvent in AutomationEvents)
            {
                Automation.AddAutomationEventHandler(automationEvent, element, TreeScope.Element, HandleAutomationEvent);
            }
        }

        public void UnregisterEventHandlers()
        {
            Automation.RemoveAllEventHandlers();
        }

        private void HandleAutomationEvent(object sender, AutomationEventArgs args)
        {
            AutomationElement element = sender as AutomationElement;

            if (args.EventId == InvokePattern.InvokedEvent)
            {
                Console.WriteLine($"Invoke() - {element.Current.Name}");
            }
            else if (element != null)
            {
                Console.WriteLine($"{args.EventId.ProgrammaticName} - {element?.Current.Name ?? ""}");
            }
        }

        private void OnFocusChanged(object src, AutomationFocusChangedEventArgs e)
        {
            AutomationElement focusedElement = src as AutomationElement;
            if (focusedElement != null)
            {
                var focusedElementCondition = Conditions.Conditions.GetConditionFromElement(focusedElement);
                Console.WriteLine($"FocusChanged - [{focusedElementCondition}]");
                if (focusedElement.GetCurrentPropertyValue(AutomationElement.NameProperty) as string == "Netflix")
                {
                    (focusedElement.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern).Invoke();
                }
            }
        }
    }
}