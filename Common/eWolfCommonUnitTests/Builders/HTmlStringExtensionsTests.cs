using FluentAssertions;
using NUnit.Framework;
using eWolfCommon.Builders;

namespace eWolfCommonUnitTests.Builders
{
    public class HTMLStringExtensionsTests
    {
        [Test]
        public void ShouldMakeTextBold()
        {
            string bold = "boldme".HTMLBold();
            bold.Should().Be("<strong>boldme</strong>");
        }

        [Test]
        public void ShouldMakeTextItalic()
        {
            string italic = "ime".HTMLItalic();
            italic.Should().Be("<i>ime</i>");
        }
    }
}
