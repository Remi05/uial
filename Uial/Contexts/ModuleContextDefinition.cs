using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uial.Modules;
using Uial.Scopes;

namespace Uial.Contexts
{
    public class ModuleContextDefinition : IContextDefinition
    {
        protected ModuleDefinition ModuleDefinition { get; set; }
        public string Name => ModuleDefinition.Name;
        public DefinitionScope Scope { get; protected set; }

        public ModuleContextDefinition(ModuleDefinition moduleDefinition)
        {
            ModuleDefinition = moduleDefinition;
        }

        public IContext Resolve(IContext parentContext, IEnumerable<string> paramValues)
        {
            var moduleProvider = new ModuleProvider();
            Module module = moduleProvider.GetModule(ModuleDefinition);
            return new ModuleContext(parentContext, module);
        }

    }
}
