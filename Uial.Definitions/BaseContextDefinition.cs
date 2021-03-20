using System;
using System.Collections.Generic;
using System.Linq;
using Uial.Scopes;

namespace Uial.Definitions
{
    public class BaseContextDefinition
    {
        public string ContextName { get; private set; }
        private IEnumerable<ValueDefinition> ParamsValueDefinitions { get; set; }
        private BaseContextDefinition Child { get; set; }

        public BaseContextDefinition(string contextName, IEnumerable<ValueDefinition> paramsValueDefinitions, BaseContextDefinition child = null)
        {
            ContextName = contextName;
            ParamsValueDefinitions = paramsValueDefinitions;
            Child = child;
        }
    }
}
