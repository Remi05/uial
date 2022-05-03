using System;
using System.Text;
using System.Threading;
using UIAutomationClient;
using Uial.Windows.Conditions;

namespace Uial.LiveConsole
{
    public class VisualTreeSerializer
    {
        private IUIAutomation UIAutomation { get; set; } = new CUIAutomation();

        public string GetVisualTreeRepresentation(IUIAutomationElement element, TreeScope treeScope)
        {
            switch (treeScope)
            {
                case TreeScope.TreeScope_Ancestors:
                    return GetAncestorsRepresentation(element);
                case TreeScope.TreeScope_Children:
                    return GetChildrenRepresentation(element);
                case TreeScope.TreeScope_Descendants:
                    return GetDescendantsRepresentation(element);
                case TreeScope.TreeScope_Element:
                    return GetElementRepresentation(element);
                case TreeScope.TreeScope_Parent:
                    return GetParentRepresentation(element);
                case TreeScope.TreeScope_Subtree:
                    return GetSubtreeRepresentation(element);
            }

            throw new Exception($"The given TreeScope \"{treeScope}\" is not supported.");
        }

        public string GetAncestorsRepresentation(IUIAutomationElement element)
        {
            string elementRepresentation = GetElementRepresentation(element);
            if (element == UIAutomation.GetRootElement())
            {
                return elementRepresentation;
            }
            // TODO: Fix indent.
            var parent = UIAutomation.CreateTreeWalker(UIAutomation.RawViewCondition).GetParentElement(element);
            return GetAncestorsRepresentation(parent) + "  |----" + elementRepresentation;
        }

        public string GetChildrenRepresentation(IUIAutomationElement element)
        {
            Thread.Sleep(2000);
            var children = element.FindAll(TreeScope.TreeScope_Children, UIAutomation.CreateTrueCondition());
            StringBuilder childrenStrBuilder = new StringBuilder();
            for (int i = 0; i < children.Length; ++i)
            {
                childrenStrBuilder.Append($"  |----" + GetElementRepresentation(children.GetElement(i)));
            }
            return childrenStrBuilder.ToString();
        }

        public string GetDescendantsRepresentation(IUIAutomationElement element)
        {
            var children = element.FindAll(TreeScope.TreeScope_Children, UIAutomation.CreateTrueCondition());
            StringBuilder descendantsStrBuilder = new StringBuilder();
            for (int i = 0; i < children.Length; ++i)
            {
                descendantsStrBuilder.Append("  |----" + GetElementRepresentation(children.GetElement(i)));
                string descendantsStr = GetDescendantsRepresentation(children.GetElement(i));
                descendantsStrBuilder.Append(descendantsStr.Replace("\n", "\n  "));
            }
            return descendantsStrBuilder.ToString();
        }

        public string GetElementRepresentation(IUIAutomationElement element)
        {
            string elementRepresentation = $"[{Conditions.GetConditionFromElement(element)}]";
            if (element == UIAutomation.GetRootElement())
            {
                elementRepresentation += " (Root)";
            }
            return elementRepresentation + "\n";
        }

        public string GetParentRepresentation(IUIAutomationElement element)
        {
            var parent = UIAutomation.CreateTreeWalker(UIAutomation.RawViewCondition).GetParentElement(element);
            return GetElementRepresentation(parent);
        }

        public string GetSubtreeRepresentation(IUIAutomationElement element)
        {
            return GetElementRepresentation(element) + GetDescendantsRepresentation(element);
        }
    }
}
