using eWolfPodcasterCore.Data;
using FluentAssertions;
using NUnit.Framework;

namespace eWolfPodcasterCoreUnitTests.Data
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