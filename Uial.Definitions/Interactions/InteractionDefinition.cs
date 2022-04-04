﻿using System;
using System.Collections.Generic;

namespace Uial.DataModels
{
    public class InteractionDefinition
    {
        public DefinitionScope Scope { get; protected set; }
        public string Name { get; protected set; }
        public IEnumerable<string> ParamNames { get; protected set; }
        public IEnumerable<BaseInteractionDefinition> BaseInteractionDefinitions { get; protected set; }

        public InteractionDefinition(DefinitionScope scope, string name, IEnumerable<string> paramNames, IEnumerable<BaseInteractionDefinition> baseInteractionDefinitions)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Scope = scope;
            Name = name;
            ParamNames = paramNames;
            BaseInteractionDefinitions = baseInteractionDefinitions;
        }
    }
}
