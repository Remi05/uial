﻿using System;
using System.Collections.Generic;
using System.Linq;
using ScenarioScripting.Contexts;
using ScenarioScripting.Interactions;

namespace ScenarioScripting.Scopes
{
    public class RuntimeScope
    {
        public Dictionary<string, object> ReferenceValues { get; private set; } = new Dictionary<string, object>();
        public Dictionary<string, IContextDefinition> ContextDefinitions { get; private set; } = new Dictionary<string, IContextDefinition>();
        public Dictionary<string, IInteractionDefinition> InteractionDefinitions { get; private set; } = new Dictionary<string, IInteractionDefinition>();

        public RuntimeScope(DefinitionScope definitionScope, Dictionary<string, object> referenceValues)
        {
            ReferenceValues = referenceValues;
            ContextDefinitions = new Dictionary<string, IContextDefinition>(definitionScope.ContextDefinitions);
            InteractionDefinitions = new Dictionary<string, IInteractionDefinition>(definitionScope.InteractionDefinitions);
        }
    }
}
