using AudioWolfStandard.Helpers;
using AudioWolfStandard.Tags;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace AudioWolfBaseUnitTests.Helpers
{
    public class TagHelperTests
    {
        [Test]
        public void ShouldCreateTagDataWithSimpleName()
        {
            TagData td = TagHelper.CreateTag("test");
            td.Should().NotBeNull();
            td.Name.Should().Be("test");
        }

        [Test]
        public void ShouldCreateTagDataWithSpacedName()
        {
            TagData td = TagHelper.CreateTag("test Some data");
            td.Should().NotBeNull();
            td.Name.Should().Be("TestSomeData");
        }

        [TestCase("Me", "Me")]
        [TestCase("MeAndYou", "MeAndYou")]
        [TestCase("Me Space", "MeSpace")]
        [TestCase("me space", "MeSpace")]
        [TestCase("Me Space me space", "MeSpaceMeSpace")]
        [TestCase("UP", "UP")]
        [TestCase("UP UP", "UpUp")]
        public void ShouldReturnSameName(string input, string expected)
        {
            TagHelper.CleanseTagName(input).Should().Be(expected);
        }

        /*[Test]
        public void Should()
        {
            List<TagData> tags = TagHelper.GetValidTags("Tag,tags");
            tags.Should().HaveCount(2);
        }*/
    }
}
