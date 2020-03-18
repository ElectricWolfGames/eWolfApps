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
    }
}
