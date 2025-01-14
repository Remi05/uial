using System.Collections.Generic;
using UIAutomationClient;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Scopes;
using Uial.Windows.Conditions;

namespace Uial.Windows.Contexts
{
    public class WindowsVisualContextProvider : IContextProvider
    {
        protected const string ProviderAlias = "UIElement";

        protected IConditionResolver ConditionResolver { get; set; }

        public WindowsVisualContextProvider(IConditionResolver conditionResolver)
        {
            ConditionResolver = conditionResolver;
        }

        public bool IsKnownContext(ContextScopeDefinition contextScopeDefinition, IContext parentContext)
        {
            return contextScopeDefinition.ContextType == ProviderAlias || Properties.IsControlTypeName(contextScopeDefinition.ContextType);
        }

        public IContext GetContextFromDefinition(ContextScopeDefinition contextScopeDefinition, IEnumerable<object> paramValues, IContext parentContext)
        {
            ConditionDefinition scopeCondition = contextScopeDefinition.ScopeCondition;
            if (Properties.IsControlTypeName(contextScopeDefinition.ContextType))
            {
                var controlTypeCondition = new PropertyConditionDefinition("ControlType", new LiteralValueDefinition(contextScopeDefinition.ContextType));
                scopeCondition = new CompositeConditionDefinition(new List<ConditionDefinition>() { scopeCondition, controlTypeCondition });
            }

            IUIAutomationCondition elementCondition = ConditionResolver.Resolve(scopeCondition, parentContext.Scope.ReferenceValueStore);
            if (parentContext == null || !(parentContext is IWindowsVisualContext))
            {
                parentContext = new RootVisualContext(parentContext.Scope);
            }
            var runtimeScope = new RuntimeScope(new DefinitionScope(new DefinitionScope()), null);
            return new WindowsVisualContext(parentContext as IWindowsVisualContext, runtimeScope, "", elementCondition); // TODO: Fix scope
        }
    }
}
