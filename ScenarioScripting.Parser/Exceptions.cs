using System;

namespace ScenarioScripting.Parser.Exceptions
{
    public class InvalidBaseInteractionException : Exception
    {
        private string BaseInteraction { get; set; }

        public override string Message => $"The given base interaction is invalid: {BaseInteraction}";

        public InvalidBaseInteractionException(string baseInteraction)
        {
            BaseInteraction = baseInteraction;
        }
    }

    public class InvalidConditionException : Exception
    {
        private string Condition { get; set; }

        public override string Message => $"The given condition is invalid: {Condition}";

        public InvalidConditionException(string condition)
        {
            Condition = condition;
        }
    }

    public class InvalidContextDeclarationException : Exception
    {
        private string ContextDeclaration { get; set; }

        public override string Message => $"The given context declaration is invalid: {ContextDeclaration}";

        public InvalidContextDeclarationException(string contextDeclaration)
        {
            ContextDeclaration = contextDeclaration;
        }
    }

    public class InvalidValueDefinitionException : Exception
    {
        private string ValueDefinition { get; set; }

        public override string Message => $"The given value definition is invalid: {ValueDefinition}";

        public InvalidValueDefinitionException(string valueDefinition)
        {
            ValueDefinition = valueDefinition;
        }
    }
}
