using eWolfPodcasterCore.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace eWolfPodcasterCoreUnitTests.Helpers
{
    public class WordParseHelperTests
    {
        [Test]
        public void ShouldSplitLines()
        {
            string words = "A Be CCc DDDD EEEEE";
            var lines = WordParseHelper.GetWordsPerLine(words, 8);
            lines.Should().HaveCount(2);
        }

        [Test]
        public void ShouldSplitSmallerLines()
        {
            string words = "Abbbbbbbb Be CCc DbbbbbbDDD EEEEE";
            var lines = WordParseHelper.GetWordsPerLine(words, 4);
            lines.Should().HaveCount(4);
        }

        [Test]
        public void ShouldSplitWords()
        {
            string words = "First Second";
            var lines = WordParseHelper.GetWordsPerLine(words, 4);
            lines.Should().HaveCount(2);
            lines[0].Should().Be("First");
            lines[1].Should().Be("Second");
        }
    }
}