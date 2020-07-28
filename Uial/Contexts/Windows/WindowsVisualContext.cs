using System;
using System.Windows.Automation;
using Uial.Scopes;

namespace Uial.Contexts
{
    public class WindowsVisualContext : AbstractContext, IContext
    {
        protected IWindowsVisualContext Parent { get; set; }
        protected Condition RootElementCondition { get; set; }

        public override AutomationElement RootElement
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
    }
}
