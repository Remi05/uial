using System.Collections.Generic;
using System.Windows.Automation;
using Uial.Conditions;

namespace Uial.Contexts
{
    public static class Controls
    {
        private static readonly Dictionary<string, AutomationProperty> AutomationPropertyMap = new Dictionary<string, AutomationProperty>()
        {
            //{ "AcceleratorKey", AutomationElement.AcceleratorKeyProperty },
            //{ "AccessKey", AutomationElement.AccessKeyProperty },
            { "AutomationId", AutomationElement.AutomationIdProperty },
            //{ "BoundingRectangle", AutomationElement.BoundingRectangleProperty },
            { "ClassName", AutomationElement.ClassNameProperty },
            //{ "ClickablePoint", AutomationElement.ClickablePointProperty },
            { "ControlType", AutomationElement.ControlTypeProperty },
            //{ "Culture", AutomationElement.CultureProperty },
            //{ "FrameworkId", AutomationElement.FrameworkIdProperty },
            { "Name", AutomationElement.NameProperty },
        };

        private static readonly Dictionary<string, ControlType> ControlTypeMap = new Dictionary<string, ControlType>()
        {
            { "Button",      ControlType.Button },
            { "Calendar",    ControlType.Calendar },
            { "CheckBox",    ControlType.CheckBox },
            { "ComboBox",    ControlType.ComboBox },
            { "Custom",      ControlType.Custom },
            { "DataGrid",    ControlType.DataGrid },
            { "DataItem",    ControlType.DataItem },
            { "Document",    ControlType.Document },
            { "Edit",        ControlType.Edit },
            { "Group",       ControlType.Group },
            { "Header",      ControlType.Header },
            { "HeaderItem",  ControlType.HeaderItem },
            { "Hyperlink",   ControlType.Hyperlink },
            { "Image",       ControlType.Image },
            { "List",        ControlType.List },
            { "ListItem",    ControlType.ListItem },
            { "Menu",        ControlType.Menu },
            { "MenuBar",     ControlType.MenuBar },
            { "MenuItem",    ControlType.MenuItem },
            { "Pane",        ControlType.Pane },
            { "ProgressBar", ControlType.ProgressBar },
            { "RadioButton", ControlType.RadioButton },
            { "ScrollBar",   ControlType.ScrollBar },
            { "Seperator",   ControlType.Separator },
            { "Slider",      ControlType.Slider },
            { "Spinner",     ControlType.Spinner },
            { "SplitButton", ControlType.SplitButton },
            { "StatusBar",   ControlType.StatusBar },
            { "Tab",         ControlType.Tab },
            { "TabItem",     ControlType.TabItem },
            { "Table",       ControlType.Table },
            { "Text",        ControlType.Text },
            { "Thumb",       ControlType.Thumb },
            { "TitleBar",    ControlType.TitleBar },
            { "ToolBar",     ControlType.ToolBar },
            { "ToolTip",     ControlType.ToolTip },
            { "Tree",        ControlType.Tree },
            { "TreeItem",    ControlType.TreeItem },
            { "Window",      ControlType.Window },
        };

        public static AutomationProperty GetPropertyByName(string propertyName)
        {
            return AutomationPropertyMap[propertyName];
        }

        public static object GetPropertyValue(AutomationProperty property, string valueStr)
        {
            if (property == AutomationElement.ControlTypeProperty)
            {
                return ControlTypeMap[valueStr];
            }
            return valueStr;
        }

        public static IConditionDefinition GetControlConditionDefinition(string controlTypeName, IConditionDefinition identifyingCondition)
        {
            ValueDefinition controlTypeRuntimeValue = ValueDefinition.FromLitteral(controlTypeName);
            IConditionDefinition controlTypeCondition = new PropertyConditionDefinition(AutomationElement.ControlTypeProperty, controlTypeRuntimeValue);
            return new CompositeConditionDefinition(new List<IConditionDefinition>() { controlTypeCondition, identifyingCondition });
        }
    }
}
