using FluentAssertions;
using NUnit.Framework;
using eWolfCommon.Builders;

namespace eWolfCommonUnitTests.Builders
{
    public class StringExtensionsTests
    {
        [Test]
        public void ShouldMakeTextBold()
        {
            string bold = "boldme".Bold();
            bold.Should().Be("<strong>boldme</strong>");
        }

        [Test]
        public void ShouldMakeTextItalic()
        {
            string italic = "ime".Italic();
            italic.Should().Be("<i>ime</i>");
        }
    }
}
