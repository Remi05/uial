using System;
using Uial.Contexts;

namespace Uial.Windows.Contexts
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
