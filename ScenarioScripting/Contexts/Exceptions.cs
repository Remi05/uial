using System;

namespace ScenarioScripting.Contexts
{
    public class ContextNotFoundException : Exception
    {
        private string ContextName { get; set; }
        private IContext CurrentContext { get; set; }

        public override string Message => $"Could not find the context \"{ContextName}\" in the current context \"{CurrentContext.Name}\"";

        public ContextNotFoundException(string contextName, IContext currentContext)
        {
            ContextName = contextName;
            CurrentContext = currentContext;
        }
    }
}
