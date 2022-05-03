using System;

namespace Uial.Contexts
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

    public class ContextUnavailableException : Exception
    {
        private string ContextName { get; set; }

        public override string Message => $"Context \"{ContextName}\" is not currently available.";

        public ContextUnavailableException(string contextName)
        {
            ContextName = contextName;
        }
    }
}
