using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.DataModels;
using Uial.Parsing.Exceptions;

namespace Uial.Parsing.UnitTests
{
    [TestClass]
    public class ValueDefinitionsParsingTests
    {
        private class Valid
        {
            public class Literals
            {
                public const string EmptyString = "\"\"";
                public const string Number = "\"15\"";
                public const string ReferenceChar = "\"$\"";
                public const string ReferenceInQuotes = "\"$testLiteral\"";
                public const string SpecialChars = "\"~`!@#%^&*()-_+=[]{}\\:;',.<>/?\"";
                public const string Text = "\"testLiteral\"";
                public const string WhiteSpace = "\"  \"";
            }

            public class References
            {
                public const string Basic = "$testReference";
                public const string AllLower = "$testreference";
                public const string AllUpper = "$TESTREFERENCE";
                public const string WithNumberAtEnd = "$testReference1";
            }
        }

        private class Invalid
        {
            public class Literals
            {
                public const string MissingQuoteAtEnd = "\"testLiteral";
                public const string MissingQuoteAtStart = "testLiteral\"";
                public const string DoubleQuotesText = "\"\"\"\"";
                public const string SingleQuoteText = "\"\"\"";
            }

            public class References
            {
                public const string Empty = "$";
                public const string MissingPrefix = "testReference";
                public const string TextBeforePrefix = "test$Reference";
                public const string WithNumberAtStart = "$1testReference";
                public const string WithNumberInMiddle = "$test1Reference";
                public const string WithQuotes = "$\"testReference\"";
                public const string WithSpaceAtStart = "$ testReference";
                public const string WithSpaceInMiddle = "$test reference";
            }
        }


        [DataRow(Valid.Literals.EmptyString,        DisplayName = "Valid_Literals_EmptyString")]
        [DataRow(Valid.Literals.Number,             DisplayName = "Valid_Literals_Number")]
        [DataRow(Valid.Literals.ReferenceChar,      DisplayName = "Valid_Literals_ReferenceChar")]
        [DataRow(Valid.Literals.ReferenceInQuotes,  DisplayName = "Valid_Literals_ReferenceInQuotes")]
        [DataRow(Valid.Literals.SpecialChars,       DisplayName = "Valid_Literals_SpecialChars")]
        [DataRow(Valid.Literals.Text,               DisplayName = "Valid_Literals_Text")]
        [DataRow(Valid.Literals.WhiteSpace,         DisplayName = "Valid_Literals_WhiteSpace")]
        [DataRow(Valid.References.Basic,            DisplayName = "Valid_References_Basic")]
        [DataRow(Valid.References.AllLower,         DisplayName = "Valid_References_AllLower")]
        [DataRow(Valid.References.AllUpper,         DisplayName = "Valid_References_AllUpper")]
        [DataRow(Valid.References.WithNumberAtEnd,  DisplayName = "Valid_References_WithNumberAtEnd")]
        [DataTestMethod]
        public void ValidValueDefinitionsCanBeParsed(string valueStr)
        {
            ScriptParser parser = new ScriptParser();
            ValueDefinition valueDefinition = parser.ParseValueDefinition(valueStr);

            Assert.IsNotNull(valueDefinition, "The parsed ValueDefinition should not be null.");
        }


        [DataRow(Invalid.Literals.MissingQuoteAtEnd,    DisplayName = "Invalid_Literals_MissingQuoteAtEnd")]
        [DataRow(Invalid.Literals.MissingQuoteAtStart,  DisplayName = "Invalid_Literals_MissingQuoteAtStart")]
        [DataRow(Invalid.Literals.DoubleQuotesText,     DisplayName = "Invalid_Literals_DoubleQuotesText")]
        [DataRow(Invalid.Literals.SingleQuoteText,      DisplayName = "Invalid_Literals_SingleQuoteText")]
        [DataRow(Invalid.References.Empty,              DisplayName = "Invalid_References_Empty")]
        [DataRow(Invalid.References.MissingPrefix,      DisplayName = "Invalid_References_MissingPrefix")]
        [DataRow(Invalid.References.TextBeforePrefix,   DisplayName = "Invalid_References_TextBeforePrefix")]
        [DataRow(Invalid.References.WithNumberAtStart,  DisplayName = "Invalid_References_WithNumberAtStart")]
        [DataRow(Invalid.References.WithNumberInMiddle, DisplayName = "Invalid_References_WithNumberInMiddle")]
        [DataRow(Invalid.References.WithQuotes,         DisplayName = "Invalid_References_WithQuotes")]
        [DataRow(Invalid.References.WithSpaceAtStart,   DisplayName = "Invalid_References_WithSpaceAtStart")]
        [DataRow(Invalid.References.WithSpaceInMiddle,  DisplayName = "Invalid_References_WithSpaceInMiddle")]
        [DataTestMethod]
        public void InvalidValueDefinitionsThrowException(string valueStr)
        {
            ScriptParser parser = new ScriptParser();
            Assert.ThrowsException<InvalidValueDefinitionException>(() => parser.ParseValueDefinition(valueStr));
        }
    }
}
