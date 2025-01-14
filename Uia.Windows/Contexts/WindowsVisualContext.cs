using System;
using UIAutomationClient;
using Uial.Scopes;

namespace Uial.Windows.Contexts
{
    public class WindowsVisualContext : IWindowsVisualContext
    { 
        protected IWindowsVisualContext Parent { get; set; }
        protected IUIAutomationCondition ElementCondition { get; set; }

        public string Name { get; protected set; }
        public RuntimeScope Scope { get; protected set; }
        public IUIAutomationElement RootElement
        {
            get
            {
                return ElementCondition == null
                     ? Parent.RootElement
                     : Parent.RootElement?.FindFirst(TreeScope.TreeScope_Subtree, ElementCondition);
            }
        }

        public WindowsVisualContext(IWindowsVisualContext parent, RuntimeScope scope, string name, IUIAutomationCondition elementCondition = null)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }
            Parent = parent;
            Scope = scope;
            Name = name;
            ElementCondition = elementCondition;
        }

        public bool IsAvailable()
        {
            return RootElement != null;
        }
    }
}
