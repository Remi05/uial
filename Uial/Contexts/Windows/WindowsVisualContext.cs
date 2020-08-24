using System;
using System.Windows.Automation;
using Uial.Scopes;

namespace Uial.Contexts
{
    public class WindowsVisualContext : IWindowsVisualContext
    { 
        protected IWindowsVisualContext Parent { get; set; }
        protected Condition RootElementCondition { get; set; }
        protected Condition UniqueCondition { get; set; }

        public string Name { get; protected set; }
        public RuntimeScope Scope { get; protected set; }
        public AutomationElement RootElement
        {
            get
            {
                return RootElementCondition == null
                     ? Parent.RootElement
                     : Parent.RootElement?.FindFirst(TreeScope.Subtree, RootElementCondition);
            }
        }

        public WindowsVisualContext(IWindowsVisualContext parent, RuntimeScope scope, string name, Condition rootElementCondition = null, Condition uniqueCondition = null)
        {
            if (parent == null)
            {
                throw new ArgumentNullException("parent");
            }
            Parent = parent;
            Scope = scope;
            Name = name;
            RootElementCondition = rootElementCondition;
            UniqueCondition = uniqueCondition;
        }

        public bool IsAvailable()
        {
            return RootElement != null
                && (UniqueCondition == null
                || RootElement.FindFirst(TreeScope.Subtree, UniqueCondition) != null);
        }
    }
}
