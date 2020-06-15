using System.Collections.Generic;
using Uial.Scopes;

namespace Uial.Contexts
{
    public interface IContextDefinition
    {
        string Name { get; }

        DefinitionScope Scope { get; }

        IContext Resolve(IContext parentContext, IEnumerable<string> paramValues);
    }
}
