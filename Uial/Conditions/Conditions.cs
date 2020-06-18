using System.Collections.Generic;
using System.Windows.Automation;

namespace Uial.Conditions
{
    public static class Properties
    {
        private static readonly Dictionary<string, AutomationProperty> AutomationPropertyMap = new Dictionary<string, AutomationProperty>()
        {
            { "AcceleratorKey",    AutomationElement.AcceleratorKeyProperty },
            { "AccessKey",         AutomationElement.AccessKeyProperty },
            { "AutomationId",      AutomationElement.AutomationIdProperty },
            { "BoundingRectangle", AutomationElement.BoundingRectangleProperty },
            { "ClassName",         AutomationElement.ClassNameProperty },
            { "ClickablePoint",    AutomationElement.ClickablePointProperty },
            { "ControlType",       AutomationElement.ControlTypeProperty },
            { "Culture",           AutomationElement.CultureProperty },
            { "FrameworkId",       AutomationElement.FrameworkIdProperty },
            { "Name",              AutomationElement.NameProperty },
        };

        public static AutomationProperty GetPropertyByName(string propertyName)
        {
            if (!AutomationPropertyMap.ContainsKey(propertyName))
            {
                throw new UnknownPropertyException(propertyName);
            }
            return AutomationPropertyMap[propertyName];
        }
    }
}
