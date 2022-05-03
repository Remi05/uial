
namespace Uial.DataModels
{
    public class BaseContextDefinition 
    {
        public ContextScopeDefinition ContextScope { get; protected set; }
        public BaseContextDefinition ChildContext { get; protected set; }

        public BaseContextDefinition(ContextScopeDefinition contextScope, BaseContextDefinition childContext = null)
        {
            ContextScope = contextScope;
            ChildContext = childContext;
        }
    }
}
