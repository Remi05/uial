
namespace Uial.DataModels
{
    public class BaseContextDefinition 
    {
        public ContextScopeDefinition ContextScope { get; private set; }
        public BaseContextDefinition ChildContext { get; private set; }

        public BaseContextDefinition(ContextScopeDefinition contextScope, BaseContextDefinition childContext = null)
        {
            ContextScope = contextScope;
            ChildContext = childContext;
        }
    }
}
