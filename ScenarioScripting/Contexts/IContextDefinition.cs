using System.Collections.Generic;
using ScenarioScripting.Scopes;

namespace ScenarioScripting.Contexts
{
    public interface IContextDefinition
    {
        string Name { get; }

        Scope Scope { get; }

        IContext Resolve(IContext parentContext, IEnumerable<object> paramValues);
    }
}
