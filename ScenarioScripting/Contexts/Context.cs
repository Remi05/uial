using System;
using System.Windows.Automation;

namespace ScenarioScripting.Contexts
{
    public class Context : AbstractContext, IContext
    {
        protected IContext Parent { get; set; }
        protected Condition RootElementCondition { get; set; }

        public override AutomationElement RootElement
        {
            get
            {
                return RootElementCondition == null
                     ? Parent.RootElement
                     : Parent.RootElement.FindFirst(TreeScope.Subtree, RootElementCondition);
            }
        }

        public Context(IContext parent, string name, Condition rootElementCondition = null, Condition uniqueCondition = null)
        {
            // throw if parent == null
            Name = name;
            Parent = parent;
            RootElementCondition = rootElementCondition;
            UniqueCondition = uniqueCondition;
        }
    }
}
