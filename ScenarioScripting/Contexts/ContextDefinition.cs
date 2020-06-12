using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using ScenarioScripting.Conditions;
using ScenarioScripting.Interactions;
using ScenarioScripting.Scopes;

namespace ScenarioScripting.Contexts
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
                throw new ArgumentNullException("name");
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

            return new Context(parentContext, runtimeScope, Name, rootElementCondition, uniqueCondition);
        }
    }
}
