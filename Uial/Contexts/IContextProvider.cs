using System.Collections.Generic;
using Uial.DataModels;

namespace Uial.Contexts
{
    public interface IContextProvider
    {
        bool IsKnownContext(ContextScopeDefinition contextScopeDefinition, IContext parentContext);

        IContext GetContextFromDefinition(ContextScopeDefinition contextScopeDefinition, IEnumerable<object> paramValues, IContext parentContext);
    };
}
