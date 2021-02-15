using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using Uial.Conditions;
using Uial.Contexts.Windows;
using Uial.Interactions;
using Uial.Scopes;

namespace Uial.Contexts
{
    public class ContextDefinition : IContextDefinition
    {
        public DefinitionScope Scope { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<string> ParamNames { get; private set; }
        private IConditionDefinition RootElementConditionDefiniton { get; set; }
        private IConditionDefinition UniqueConditionDefinition { get; set; }

        public ContextDefinition(DefinitionScope scope, string name, IEnumerable<string> paramNames, IConditionDefinition rootElementConditionDefiniton = null, IConditionDefinition uniqueConditionDefinition = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }    
            Scope = scope;
            Name = name;
            ParamNames = paramNames;
            RootElementConditionDefiniton = rootElementConditionDefiniton;
            UniqueConditionDefinition = uniqueConditionDefinition;
        }

        public IContext Resolve(IContext parentContext, IEnumerable<string> paramValues)
        {
            if (paramValues.Count() != ParamNames.Count())
            {
                throw new InvalidParameterCountException(ParamNames.Count(), paramValues.Count());
            }

            Dictionary<string, string> referenceValues = new Dictionary<string, string>(parentContext.Scope.ReferenceValues);
            for (int i = 0; i < ParamNames.Count(); ++i)
            {
                referenceValues[ParamNames.ElementAt(i)] = paramValues.ElementAt(i);
            }

            RuntimeScope runtimeScope = new RuntimeScope(Scope, referenceValues);
            Condition rootElementCondition = RootElementConditionDefiniton?.Resolve(runtimeScope);
            Condition uniqueCondition = UniqueConditionDefinition?.Resolve(runtimeScope);

            return new WindowsVisualContext(parentContext as IWindowsVisualContext, runtimeScope, Name, rootElementCondition, uniqueCondition);
        }
    }
}
