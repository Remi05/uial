using System;
using System.Collections.Generic;
using System.Text;
using UIAutomationClient;
using Uial.Contexts;
using Uial.DataModels;
using Uial.Windows.Conditions;

namespace Uial.Windows.Contexts
{
    public class WindowsVisualContextProvider : IContextProvider
    {
        protected const string ProviderAlias = "UIElement";

        protected IConditionResolver ConditionResolver { get; set; }

        public bool IsKnownContext(ContextScopeDefinition contextScopeDefinition, IContext parentContext)
        {
            return contextScopeDefinition.ContextType == ProviderAlias;
        }

        public IContext GetContextFromDefinition(ContextScopeDefinition contextScopeDefinition, IEnumerable<object> paramValues, IContext parentContext)
        {
            IUIAutomationCondition elementCondition = ConditionResolver.Resolve(contextScopeDefinition.ScopeCondition, parentContext.Scope.ReferenceValueStore);
            if (parentContext == null || !(parentContext is IWindowsVisualContext))
            {
                // TOOD: Create root visual context
            }
            return new WindowsVisualContext(parentContext as IWindowsVisualContext, null, "", elementCondition); // TODO: Fix
        }
    }
}
