using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uial.Parsing.Exceptions;

namespace Uial.Parsing.UnitTests
{
    [TestClass]
    public class ValueDefinitionsParsingTests
    {
        private class Valid
        {
            public class Litterals
            {
                public const string EmptyString = "\"\"";
                public const string Number = "\"15\"";
                public const string ReferenceChar = "\"$\"";
                public const string ReferenceInQuotes = "\"$testLitteral\"";
                public const string SpecialChars = "\"~`!@#%^&*()-_+=[]{}\\:;',.<>/?\"";
                public const string Text = "\"testLitteral\"";
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
            public class Litterals
            {
                public const string MissingQuoteAtEnd = "\"testLitteral";
                public const string MissingQuoteAtStart = "testLitteral\"";
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


        [DataRow(Valid.Litterals.EmptyString,       DisplayName = "Valid_Litterals_EmptyString")]
        [DataRow(Valid.Litterals.Number,            DisplayName = "Valid_Litterals_Number")]
        [DataRow(Valid.Litterals.ReferenceChar,     DisplayName = "Valid_Litterals_ReferenceChar")]
        [DataRow(Valid.Litterals.ReferenceInQuotes, DisplayName = "Valid_Litterals_ReferenceInQuotes")]
        [DataRow(Valid.Litterals.SpecialChars,      DisplayName = "Valid_Litterals_SpecialChars")]
        [DataRow(Valid.Litterals.Text,              DisplayName = "Valid_Litterals_Text")]
        [DataRow(Valid.Litterals.WhiteSpace,        DisplayName = "Valid_Litterals_WhiteSpace")]
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


        [DataRow(Invalid.Litterals.MissingQuoteAtEnd,   DisplayName = "Invalid_Litterals_MissingQuoteAtEnd")]
        [DataRow(Invalid.Litterals.MissingQuoteAtStart, DisplayName = "Invalid_Litterals_MissingQuoteAtStart")]
        [DataRow(Invalid.Litterals.DoubleQuotesText,    DisplayName = "Invalid_Litterals_DoubleQuotesText")]
        [DataRow(Invalid.Litterals.SingleQuoteText,     DisplayName = "Invalid_Litterals_SingleQuoteText")]
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
