using NUnit.Framework;
using FluentAssertions;
using eWolfPodcaster.Helpers;
using System.Collections.Generic;

namespace eWolfPodcasterUI.Helpers
{
    public class DataCleansingTests
    {
        [Test]
        public void ShouldRemoveAllStrings()
        {
            string str = "All and Everything else";
            str = DataCleansing.RemoveAllStrings(str, new List<string>() { "and", "else" });

            str.Should().Be("All  Everything ");
        }

        [Test]
        public void ShouldRemoveManyStrings()
        {
            string str = "All and and and Everything else";
            str = DataCleansing.RemoveAllStrings(str, new List<string>() { "and", "else" });

            str.Should().Be("All    Everything ");
        }

        [TestCase("A  B", "A B")]
        [TestCase("Words with single and      lots    of spaces", "Words with single and lots of spaces")]
        public void ShouldRemoveDoubleSpaces(string starting, string expected)
        {
            string end = DataCleansing.RemoveDoubleSpaces(starting);

            end.Should().Be(expected);
        }
    }
}
