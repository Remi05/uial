using System;

namespace Uial.Contexts.Windows
{
    public class InvalidWindowsVisualContextException : Exception
    {
        private IContext Context { get; set; }

        public override string Message => $"The given context \"{Context.Name}\" is not a valid Windows visual context.";

        public InvalidWindowsVisualContextException(IContext context)
        {
            Context = context;
        }
    }
}
