using System;

namespace ScenarioScripting.Parser.Exceptions
{
    public class InvalidContextDeclarationException : Exception
    {
        private string ContextDeclaration { get; set; }

        public override string Message => $"The given context declaration is invalid:\n{ContextDeclaration}";

        public InvalidContextDeclarationException(string contextDeclaration)
        {
            ContextDeclaration = contextDeclaration;
        }
    }
}
