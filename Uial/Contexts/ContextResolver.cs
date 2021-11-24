using System.Collections.Generic;
using System.Linq;
using UIAutomationClient;
using Uial.Conditions;
using Uial.Contexts.Windows;
using Uial.Definitions;
using Uial.Interactions;
using Uial.Scopes;

namespace Uial.Contexts
{
    public class ContextResolver : IContextResolver
    {
        private IConditionResolver ConditionResolver { get; set; }

        public ContextResolver(IConditionResolver conditionResolver)
        {
            ConditionResolver = conditionResolver;
        }

        public IContext Resolve(ContextDefinition contextDefinition, IContext parentContext, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != contextDefinition.ParamNames.Count())
            {
                throw new InvalidParameterCountException(contextDefinition.ParamNames.Count(), paramValues.Count());
            }

            Dictionary<string, string> referenceValues = new Dictionary<string, string>(parentContext.Scope.ReferenceValues);
            for (int i = 0; i < contextDefinition.ParamNames.Count(); ++i)
            {
                referenceValues[contextDefinition.ParamNames.ElementAt(i)] = paramValues.ElementAt(i);
            }

            RuntimeScope runtimeScope = new RuntimeScope(contextDefinition.Scope, referenceValues);
            IUIAutomationCondition rootElementCondition = ConditionResolver.Resolve(contextDefinition.RootElementConditionDefiniton, runtimeScope);
            IUIAutomationCondition uniqueCondition = ConditionResolver.Resolve(contextDefinition.UniqueConditionDefinition, runtimeScope);

            return new WindowsVisualContext(parentContext as IWindowsVisualContext, runtimeScope, contextDefinition.ContextName, rootElementCondition, uniqueCondition);
        }
    }
}
