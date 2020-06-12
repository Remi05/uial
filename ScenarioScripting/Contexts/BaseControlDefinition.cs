using System.Windows.Automation;
using ScenarioScripting.Conditions;
using ScenarioScripting.Scopes;

namespace ScenarioScripting.Contexts
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
            IContext context = new Context(parentContext, currentScope, ControlTypeName, condition);
            return Child?.Resolve(context, currentScope) ?? context;
        }
    }
}
