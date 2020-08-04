using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uial.Contexts;
using Uial.Scopes;

namespace Uial.LiveConsole
{
    public class ExecutionContext
    {
        public Script Script { get; protected set; }
        public RuntimeScope RootScope { get; protected set; }
        public IContext RootContext { get; protected set; }

        public ExecutionContext()
        {
            Script = new Script();
            RootScope = new RuntimeScope(Script.RootScope, new Dictionary<string, string>());
            RootContext = new RootContext(RootScope);
        }
    }
}
