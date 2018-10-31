using AudioWolfUI.Tags;
using FluentAssertions;
using NUnit.Framework;

namespace AudioWolfStandardUnitTests.Tags
{
    public class TagHolderTests
    {
        [Test]
        public void ShouldAddTags()
        {
            TagOptions tagOptions = new TagOptions();

            TagHolder th = new TagHolder(tagOptions);
            th.Tags.Should().HaveCount(0);
            th.Add("TagName");
            th.Tags.Should().HaveCount(1);
        }

        [Test]
        public void ShouldCreateNameFromTags()
        {
            TagOptions tagOptions = new TagOptions();

            TagHolder th = new TagHolder(tagOptions);
            th.SplitName("My First Tags");

            th.Tags.Should().HaveCount(3);
            string name = th.CreateNameFromTags();
            name.Should().Be("My First Tags");
        }

        [Test]
        public void ShouldKeepFirstPartOfName()
        {
            TagOptions tagOptions = new TagOptions();
            tagOptions.KeepFirstPartOfName = true;

            TagHolder th = new TagHolder(tagOptions);
            th.SplitName("PIC00001 With Tags");

            th.Tags.Should().HaveCount(2);
            string name = th.CreateNameFromTags();
            name.Should().Be("PIC00001 With Tags");
        }

        [Test]
        public void ShouldNotAddSameTagTwice()
        {
            TagOptions tagOptions = new TagOptions();

            TagHolder th = new TagHolder(tagOptions);
            th.SplitName("My My My");

            th.Tags.Should().HaveCount(1);

            th.Tags[0].Name.Should().Be("My");
        }

        [Test]
        public void ShouldSplitPipedString()
        {
            TagOptions tagOptions = new TagOptions();
            tagOptions.Seperator = '|';

            TagHolder th = new TagHolder(tagOptions);
            th.SplitName("My|First|Tags");

            th.Tags.Should().HaveCount(3);

            th.Tags[0].Name.Should().Be("My");
            th.Tags[1].Name.Should().Be("First");
            th.Tags[2].Name.Should().Be("Tags");
        }

        [TestCase("My First Tags")]
        [TestCase("My  First Tags")]
        [TestCase("My First  Tags")]
        [TestCase(" My First Tags")]
        [TestCase("My First Tags ")]
        [TestCase("   My  First    Tags   ")]
        public void ShouldSplitString(string name)
        {
            TagOptions tagOptions = new TagOptions();

            TagHolder th = new TagHolder(tagOptions);
            th.SplitName(name);

            th.Tags.Should().HaveCount(3);

            th.Tags[0].Name.Should().Be("My");
            th.Tags[1].Name.Should().Be("First");
            th.Tags[2].Name.Should().Be("Tags");
        }
    }
}
