using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterCore.Services;
using eWolfPodcasterCoreUnitTests.Data;
using FluentAssertions;
using NUnit.Framework;

namespace eWolfPodcasterCoreUnitTests.Services
{
    public class ServiceLocatorTests
    {
        [Test]
        public void ShouldReturnSameTypeWhenRequestedShowLibraryService()
        {
            var ch = ServiceLocator.Instance.GetService<ShowLibraryService>();
            ch.Should().BeOfType(typeof(ShowLibraryService));
        }

        [Test]
        public void ShouldReturnSameTypeWhenRequestedShows()
        {
            var ch = ServiceLocator.Instance.GetService<IShows>();
            ch.Should().BeOfType(typeof(FakeShows));
        }
    }
}