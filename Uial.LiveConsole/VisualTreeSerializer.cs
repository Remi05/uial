using System;
using System.Text;
using System.Threading;
using System.Windows.Automation;

namespace Uial.LiveConsole
{
    public class VisualTreeSerializer
    {
        public string GetVisualTreeRepresentation(AutomationElement element, TreeScope treeScope)
        {
            switch (treeScope)
            {
                case TreeScope.Ancestors:
                    return GetAncestorsRepresentation(element);
                case TreeScope.Children:
                    return GetChildrenRepresentation(element);
                case TreeScope.Descendants:
                    return GetDescendantsRepresentation(element);
                case TreeScope.Element:
                    return GetElementRepresentation(element);
                case TreeScope.Parent:
                    return GetParentRepresentation(element);
                case TreeScope.Subtree:
                    return GetSubtreeRepresentation(element);
            }

            throw new Exception($"The given TreeScope \"{treeScope}\" is not supported.");
        }

        public string GetAncestorsRepresentation(AutomationElement element)
        {
            string elementRepresentation = GetElementRepresentation(element);
            if (element == AutomationElement.RootElement)
            {
                return elementRepresentation;
            }
            var parent = TreeWalker.RawViewWalker.GetParent(element);
            // TODO: Fix indent.
            return GetAncestorsRepresentation(parent) + "\n  |----" + elementRepresentation;
        }

        public string GetChildrenRepresentation(AutomationElement element)
        {
            Thread.Sleep(2000);
            var children = element.FindAll(TreeScope.Children, Condition.TrueCondition);
            StringBuilder childrenStrBuilder = new StringBuilder();
            foreach (AutomationElement child in children)
            {
                childrenStrBuilder.Append($"  |----[{Helper.GetConditionFromElement(child)}]\n");
            }
            return childrenStrBuilder.ToString();
        }

        public string GetDescendantsRepresentation(AutomationElement element)
        {
            var children = element.FindAll(TreeScope.Children, Condition.TrueCondition);
            StringBuilder descendantsStrBuilder = new StringBuilder();
            foreach (AutomationElement child in children)
            {
                descendantsStrBuilder.Append("  |----" + GetElementRepresentation(child) + "\n");
                string descendantsStr = GetDescendantsRepresentation(child);
                descendantsStrBuilder.Append(descendantsStr.Replace("\n", "\n  "));
            }
            return descendantsStrBuilder.ToString();
        }

        public string GetElementRepresentation(AutomationElement element)
        {
            return $"[{Helper.GetConditionFromElement(element)}]";
        }

        public string GetParentRepresentation(AutomationElement element)
        {
            var parent = TreeWalker.RawViewWalker.GetParent(element);
            return $"[{Helper.GetConditionFromElement(parent)}]";
        }

        public string GetSubtreeRepresentation(AutomationElement element)
        {
            return GetElementRepresentation(element) + GetSubtreeRepresentation(element);
        }
    }
}
