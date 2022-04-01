using eWolfPodcasterCore.Helpers;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace eWolfPodcasterCoreUnitTests.Helpers
{
    public class DataCleansingTests
    {
        [TestCase("", "")]
        [TestCase(":", "-")]
        [TestCase("a?b", "a-b")]
        [TestCase("a:?:b", "a---b")]
        [TestCase("a:\\:b", "a---b")]
        public void ShouldMakeFilenameSafe(string text, string expected)
        {
            DataCleansing.FileSafeFileName(text).Should().Be(expected);
        }

        [Test]
        public void ShouldRemoveAllStrings()
        {
            string str = "All and Everything else";
            str = DataCleansing.RemoveAllStrings(str, new List<string>() { "and", "else" });

            str.Should().Be("All  Everything ");
        }

        [TestCase("A  B", "A B")]
        [TestCase("Words with single and      lots    of spaces", "Words with single and lots of spaces")]
        public void ShouldRemoveDoubleSpaces(string starting, string expected)
        {
            string end = DataCleansing.RemoveDoubleSpaces(starting);

            end.Should().Be(expected);
        }

        [Test]
        public void ShouldRemoveManyStrings()
        {
            string str = "All and and and Everything else";
            str = DataCleansing.RemoveAllStrings(str, new List<string>() { "and", "else" });

            str.Should().Be("All    Everything ");
        }
    }
}