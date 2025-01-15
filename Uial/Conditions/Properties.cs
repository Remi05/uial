using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;

using AutomationIdentifier = System.Int32;
using AutomationPropertyIdentifier = System.Int32;
using AutomationControlTypeIdentifier = System.Int32;

namespace Uial.Conditions
{
    public static class Properties
    {
        private static readonly IDictionary<string, AutomationPropertyIdentifier> AutomationPropertiesMap = new Dictionary<string, AutomationPropertyIdentifier>()
        {
            { "AcceleratorKey",    UIA_PropertyIds.UIA_AcceleratorKeyPropertyId },
            { "AccessKey",         UIA_PropertyIds.UIA_AccessKeyPropertyId },
            { "AutomationId",      UIA_PropertyIds.UIA_AutomationIdPropertyId },
            { "BoundingRectangle", UIA_PropertyIds.UIA_BoundingRectanglePropertyId },
            { "ClassName",         UIA_PropertyIds.UIA_ClassNamePropertyId },
            { "ClickablePoint",    UIA_PropertyIds.UIA_ClickablePointPropertyId },
            { "ControlType",       UIA_PropertyIds.UIA_ControlTypePropertyId },
            { "Culture",           UIA_PropertyIds.UIA_CulturePropertyId },
            { "FrameworkId",       UIA_PropertyIds.UIA_FrameworkIdPropertyId },
            { "Name",              UIA_PropertyIds.UIA_NamePropertyId },
        };

        private static readonly Dictionary<string, AutomationControlTypeIdentifier> ControlTypeMap = new Dictionary<string, AutomationControlTypeIdentifier>()
        {
            { "Button",      UIA_ControlTypeIds.UIA_ButtonControlTypeId },
            { "Calendar",    UIA_ControlTypeIds.UIA_CalendarControlTypeId },
            { "CheckBox",    UIA_ControlTypeIds.UIA_CheckBoxControlTypeId },
            { "ComboBox",    UIA_ControlTypeIds.UIA_ComboBoxControlTypeId },
            { "Custom",      UIA_ControlTypeIds.UIA_CustomControlTypeId },
            { "DataGrid",    UIA_ControlTypeIds.UIA_DataGridControlTypeId },
            { "DataItem",    UIA_ControlTypeIds.UIA_DataItemControlTypeId },
            { "Document",    UIA_ControlTypeIds.UIA_DocumentControlTypeId },
            { "Edit",        UIA_ControlTypeIds.UIA_EditControlTypeId },
            { "Group",       UIA_ControlTypeIds.UIA_GroupControlTypeId },
            { "Header",      UIA_ControlTypeIds.UIA_HeaderControlTypeId },
            { "HeaderItem",  UIA_ControlTypeIds.UIA_HeaderItemControlTypeId },
            { "Hyperlink",   UIA_ControlTypeIds.UIA_HyperlinkControlTypeId },
            { "Image",       UIA_ControlTypeIds.UIA_ImageControlTypeId },
            { "List",        UIA_ControlTypeIds.UIA_ListControlTypeId },
            { "ListItem",    UIA_ControlTypeIds.UIA_ListItemControlTypeId },
            { "Menu",        UIA_ControlTypeIds.UIA_MenuControlTypeId },
            { "MenuBar",     UIA_ControlTypeIds.UIA_MenuBarControlTypeId },
            { "MenuItem",    UIA_ControlTypeIds.UIA_MenuItemControlTypeId },
            { "Pane",        UIA_ControlTypeIds.UIA_PaneControlTypeId },
            { "ProgressBar", UIA_ControlTypeIds.UIA_ProgressBarControlTypeId },
            { "RadioButton", UIA_ControlTypeIds.UIA_RadioButtonControlTypeId },
            { "ScrollBar",   UIA_ControlTypeIds.UIA_ScrollBarControlTypeId },
            { "Seperator",   UIA_ControlTypeIds.UIA_SeparatorControlTypeId },
            { "Slider",      UIA_ControlTypeIds.UIA_SliderControlTypeId },
            { "Spinner",     UIA_ControlTypeIds.UIA_SpinnerControlTypeId },
            { "SplitButton", UIA_ControlTypeIds.UIA_SplitButtonControlTypeId },
            { "StatusBar",   UIA_ControlTypeIds.UIA_StatusBarControlTypeId },
            { "Tab",         UIA_ControlTypeIds.UIA_TabControlTypeId },
            { "TabItem",     UIA_ControlTypeIds.UIA_TabItemControlTypeId },
            { "Table",       UIA_ControlTypeIds.UIA_TableControlTypeId },
            { "Text",        UIA_ControlTypeIds.UIA_TextControlTypeId },
            { "Thumb",       UIA_ControlTypeIds.UIA_ThumbControlTypeId },
            { "TitleBar",    UIA_ControlTypeIds.UIA_TitleBarControlTypeId },
            { "ToolBar",     UIA_ControlTypeIds.UIA_ToolBarControlTypeId },
            { "ToolTip",     UIA_ControlTypeIds.UIA_ToolTipControlTypeId },
            { "Tree",        UIA_ControlTypeIds.UIA_TreeControlTypeId },
            { "TreeItem",    UIA_ControlTypeIds.UIA_TreeItemControlTypeId },
            { "Window",      UIA_ControlTypeIds.UIA_WindowControlTypeId },
        };

        public static AutomationPropertyIdentifier GetPropertyByName(string propertyName)
        {
            if (!AutomationPropertiesMap.ContainsKey(propertyName))
            {
                throw new UnknownPropertyException(propertyName);
            }
            return AutomationPropertiesMap[propertyName];
        }

        public static object GetPropertyValue(AutomationPropertyIdentifier property, string valueStr)
        {
            if (property == UIA_PropertyIds.UIA_ControlTypePropertyId)
            {
                return ControlTypeMap[valueStr];
            }
            return valueStr;
        }

        public static string GetPropertyUialString(this AutomationIdentifier property)
        {
            return AutomationPropertiesMap.Keys.Where((string propertyName) => AutomationPropertiesMap[propertyName] == property).Single(); ;
        }

        public static bool IsControlTypeIdentifier(AutomationIdentifier identifier)
        {
            return ControlTypeMap.Values.Contains(identifier);
        }

        public static string GetControlTypeUialString(this AutomationIdentifier controlType)
        {
            return !IsControlTypeIdentifier(controlType) ? null : ControlTypeMap.Keys.Where((string controlTypeName) => ControlTypeMap[controlTypeName] == controlType).Single();
        }
    }
}
