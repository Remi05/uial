using System;
using UIAutomationClient;
using Uial.Scopes;

namespace Uial.Contexts.Windows
{
    public class WindowsVisualContext : IWindowsVisualContext
    { 
        protected IWindowsVisualContext Parent { get; set; }
        protected IUIAutomationCondition RootElementCondition { get; set; }
        protected IUIAutomationCondition UniqueCondition { get; set; }

        public string Name { get; protected set; }
        public RuntimeScope Scope { get; protected set; }
        public IUIAutomationElement RootElement
        {
            get
            {
                return RootElementCondition == null
                     ? Parent.RootElement
                     : Parent.RootElement?.FindFirst(TreeScope.TreeScope_Subtree, RootElementCondition);
            }
        }

        public WindowsVisualContext(IWindowsVisualContext parent, RuntimeScope scope, string name, IUIAutomationCondition rootElementCondition = null, IUIAutomationCondition uniqueCondition = null)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
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
                || RootElement.FindFirst(TreeScope.TreeScope_Subtree, UniqueCondition) != null);
        }
    }
}
