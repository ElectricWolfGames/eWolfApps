using eWolfCommon.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace eWolfCommonUnitTests.Helpers
{
    public class TextHelperTests
    {
        [TestCase("Text", "Text")]
        [TestCase("TextMoreWords", "Text more words")]
        [TestCase("TextMore Words", "Text more Words")]
        public void ShouldConvertToSentenceCase(string from, string to)
        {
            string text = TextHelper.ToSentenceCase(from);
            text.Should().Be(to);
        }

        [TestCase("Text", "Text")]
        [TestCase("Text more words", "Text_more_words")]
        [TestCase("TextMore Words", "TextMore_words")]
        [TestCase("Text-More Words", "Text_more_words")]
        [TestCase("dot.dot", "dot_dot")]
        [TestCase("Can't", "Cant")]
        public void ShouldConvertTextToUnderscores(string from, string to)
        {
            string text = TextHelper.ConvertTextToUnderscores(from);
            text.Should().Be(to);
        }

        [TestCase("value", "const string value = \"value\";")]
        [TestCase("Value With Spaces", "const string valueWithSpaces = \"Value With Spaces\";")]
        public void ShouldConvertTextToStringVar(string from, string to)
        {
            string text = TextHelper.ConvertTextToStringVar(from);
            text.Should().Be(to);
        }
    }
}