﻿using eWolfBootstrap.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace eWolfBootstrapTests.Helpers
{
    public class HTMLRemoverTests
    {
        [Test]
        [TestCase("<abbr >More text here</abbr>", "More text here")]
        [TestCase("<abbr>More text here</abbr>", "More text here")]
        [TestCase("<abbr title=\'Side tank\'>T</abbr>", "T")]
        public void RemoveAbbrKeepInnerTests(string html, string expected)
        {
            string results = HTMLRemover.RemoveAbbrKeepInner(html);
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
        [TestCase("<abbr>T</abbr>", "T")]
        [TestCase("<abbr title=\'Side tank\'>T</abbr>", "T")]
        public void ShouldGetTextFromTagPairTests(string html, string expected)
        {
            string text = HTMLRemover.GetTextFromTagPair(html);
            text.Should().Be(expected);
        }
    }
}
