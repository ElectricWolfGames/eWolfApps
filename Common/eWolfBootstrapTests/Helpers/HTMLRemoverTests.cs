using eWolfBootstrap.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace eWolfBootstrapTests.Helpers
{
    public class HTMLRemoverTests
    {
        [Test]
        [TestCase("<abbr >More text here</abbr>", "abbr", "More text here")]
        [TestCase("<abbr>More text here</abbr>", "abbr", "More text here")]
        [TestCase("<abbr title=\'Side tank\'>T</abbr>", "abbr", "T")]
        public void RemoveAbbrKeepInnerTests(string html, string tagName, string expected)
        {
            string results = HTMLRemover.RemoveKeepInner(html, tagName);
            results.Should().Be(expected);
        }

        [Test]
        public void ShouldCreteTagGroupForOneTagTests()
        {
            string html = "<abbr title=\'Side tank\'>T</abbr>";
            string[] array = HTMLRemover.CreateTagGroups(html);
            array.Should().HaveCount(1);
            array[0].Should().Be(html);
        }

        [Test]
        public void ShouldCreteTagGroupForTwoTagTests()
        {
            string html = "<span class=\'nowrap\'></span><abbr title=\'Side tank\'>T</abbr>";
            string[] array = HTMLRemover.CreateTagGroups(html);
            array.Should().HaveCount(2);
            array[0].Should().Be("<span class=\'nowrap\'></span>");
            array[1].Should().Be("<abbr title=\'Side tank\'>T</abbr>");
        }

        [Test]
        [TestCase("<th>here</th>", "here")]
        [TestCase("<th colspan=\"2\" style=\"text-align:center;font-size:125%;font-weight:bold\">GCR Class 1B LNER Class L1 (later L3)</th>",
            "GCR Class 1B LNER Class L1 (later L3)")]
        public void ShouldFindTextBetweenTags(string html, string expected)
        {
            string results = HTMLRemover.GetTextBetweenTags(html);
            results.Should().Be(expected);
        }

        [Test]
        [TestCase("<abbr>T</abbr>", "T")]
        [TestCase("<abbr title=\'Side tank\'>T</abbr>", "T")]
        public void ShouldGetTextFromTagPairTests(string html, string expected)
        {
            string text = HTMLRemover.GetTextFromTagPair(html);
            text.Should().Be(expected);
        }

        [Test]
        [TestCase("More text</ br> here", "br", "More text here")]
        public void ShouldRemoveAnyTags(string html, string tagName, string expected)
        {
            string results = HTMLRemover.RemoveAnyTags(html, tagName);
            results.Should().Be(expected);
        }
    }
}