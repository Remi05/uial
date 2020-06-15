using System;

namespace Uial
{
    public class ReferenceValueNotFoundException : Exception
    {
        private string ReferenceName { get; set; }

        public override string Message => $"A reference value with the name \"{ReferenceName}\" does not exist in the current context.";

        public ReferenceValueNotFoundException(string referenceName)
        {
            ReferenceName = referenceName;
        }
    }
}
