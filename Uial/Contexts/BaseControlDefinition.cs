﻿using System.Windows.Automation;
using Uial.Conditions;
using Uial.Scopes;

namespace Uial.Contexts
{
    public class BaseControlDefinition : IBaseContextDefinition
    {
        private string ControlTypeName { get; set; }
        private IConditionDefinition ConditionDefinition { get; set; }
        private IBaseContextDefinition Child { get; set; }

        public BaseControlDefinition(string controlTypeName, IConditionDefinition identifyingConditionDefinition, IBaseContextDefinition child = null)
        {
            ControlTypeName = controlTypeName;
            ConditionDefinition = Controls.GetControlConditionDefinition(ControlTypeName, identifyingConditionDefinition);
            Child = child;
        }

        public IContext Resolve(IContext parentContext, RuntimeScope currentScope)
        {
            Condition condition = ConditionDefinition.Resolve(currentScope);
            IContext context = new WindowsVisualContext(parentContext as IWindowsVisualContext, currentScope, ControlTypeName, condition);
            return Child?.Resolve(context, currentScope) ?? context;
        }
    }
}
