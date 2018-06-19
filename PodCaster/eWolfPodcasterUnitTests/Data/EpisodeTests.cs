﻿using eWolfPodcaster.Data;
using FluentAssertions;
using NUnit.Framework;

namespace eWolfPodcasterUnitTests
{
    [TestFixture]
    public class EpisodeTests
    {
        [Test]
        public void ShouldHavePlayerDetailsMyDefault()
        {
            Episode e = new Episode();
            e.PlayedDetails.Should().NotBeNull();
        }
    }
}
