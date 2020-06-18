using System;

namespace Uial.Assertions
{
    public class UnknownAssertionException : Exception
    {
        private string AssertionName { get; set; }

        public override string Message => $"No assertion named {AssertionName} found.";
        
        public UnknownAssertionException(string assertionName)
        {
            AssertionName = assertionName;
        }
    }
}
