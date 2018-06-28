using eWolfPodcaster.Helpers;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace eWolfPodcasterUI.Helpers
{
    public class DataHelperTests
    {
        [TestCase("Sat, 31 Dec 2016 00:08:57 +0000", "31/12/2016 00:08:57")]
        [TestCase("Sun, 08 Jan 2017 06:47:50 GMT", "08/01/2017 06:47:50")]
        [TestCase("Tue, 18 May 2015 18:00:01 GMT", "18/05/2015 19:00:01")]
        public void ShouldParseValidDates(string date, string expected)
        {
            DateTime dt = DataHelper.ParseDate(date);
            dt.ToString("dd/MM/yyyy HH:mm:ss").Should().Be(expected);
        }
    }
}
