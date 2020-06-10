using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScenarioScripting.Contexts
{
    class ExecutionContext
    {
        public Dictionary<string, IContext> ChildrenContexts { get; private set; }
    }
}
