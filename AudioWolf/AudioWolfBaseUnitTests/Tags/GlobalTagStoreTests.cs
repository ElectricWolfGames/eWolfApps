using AudioWolfStandard.Services;
using AudioWolfUI.Tags;
using FluentAssertions;
using NUnit.Framework;

namespace AudioWolfStandardUnitTests.Tags
{
    public class GlobalTagStoreTests
    {
        [Test]
        public void ShouldAddTagToGlobal()
        {
            GlobalTagStore gts = ServiceLocator.Instance.GetService<GlobalTagStore>();
            gts.ClearTags();

            TagOptions tagOptions = new TagOptions();

            TagHolder th = new TagHolder(tagOptions);
            th.SplitName("TasgA TagB");

            gts.Tags.Should().HaveCount(2);
        }
    }
}
