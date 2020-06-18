using System;

namespace Uial.Parsing.Exceptions
{
    public class UnrecognizedPatternExeception : Exception
    {
        private string Pattern { get; set; }

        public override string Message => $"The given base interaction is invalid: {Pattern}";

        public UnrecognizedPatternExeception(string pattern)
        {
            Pattern = pattern;
        }
    }

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

    public class InvalidContextDefinitionException : Exception
    {
        private string ContextDefinition { get; set; }

        public override string Message => $"The given context definition is invalid: {ContextDefinition}";

        public InvalidContextDefinitionException(string contextDefinition)
        {
            ContextDefinition = contextDefinition;
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

    public class InvalidTestDefinitionException : Exception
    {
        private string TestDefinition { get; set; }

        public override string Message => $"The given test definition is invalid: {TestDefinition}";

        public InvalidTestDefinitionException(string testDefinition)
        {
            TestDefinition = testDefinition;
        }
    }

    public class InvalidTestGroupDeclarationException : Exception
    {
        private string TestGroupDeclaration { get; set; }

        public override string Message => $"The given test group declaration is invalid: {TestGroupDeclaration}";

        public InvalidTestGroupDeclarationException(string testGroupDeclaration)
        {
            TestGroupDeclaration = testGroupDeclaration;
        }
    }

    public class InvalidTestGroupDefinitionException : Exception
    {
        private string TestGroupDefinition { get; set; }

        public override string Message => $"The given test group definition is invalid: {TestGroupDefinition}";

        public InvalidTestGroupDefinitionException(string testGroupDefinition)
        {
            TestGroupDefinition = testGroupDefinition;
        }
    }
}
