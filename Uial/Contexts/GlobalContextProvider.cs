using System.Collections.Generic;
using System.Linq;
using Uial.DataModels;

namespace Uial.Contexts
{
    public class GlobalContextProvider : IContextProvider
    {
        protected ISet<IContextProvider> ContextProviders { get; set; } = new HashSet<IContextProvider>();

        public void AddProvider(IContextProvider contextProvider)
        {
            ContextProviders.Add(contextProvider);
        }

        public void AddMultipleProviders(ICollection<IContextProvider> contextProviders)
        {
            foreach (var contextProvider in contextProviders)
            {
                ContextProviders.Add(contextProvider);
            }
        }

        public bool IsKnownContext(ContextScopeDefinition contextScopeDefinition, IContext parentContext)
        {
            return ContextProviders.Any(contextProvider => contextProvider.IsKnownContext(contextScopeDefinition, parentContext));
        }

        public IContext GetContextFromDefinition(ContextScopeDefinition contextScopeDefinition, IEnumerable<object> paramValues, IContext parentContext)
        {
            if (!IsKnownContext(contextScopeDefinition, parentContext))
            {
                throw new ContextNotFoundException(contextScopeDefinition.ContextType, parentContext);
            }
            IContextProvider contextProvider = ContextProviders.First(provider => provider.IsKnownContext(contextScopeDefinition, parentContext));
            return contextProvider.GetContextFromDefinition(contextScopeDefinition, paramValues, parentContext);
        }
    }
}
