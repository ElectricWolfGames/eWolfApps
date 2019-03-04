using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Services;
using FluentAssertions;
using NUnit.Framework;

namespace eWolfPodcasterCoreUnitTests.Services
{
    public class ServiceLocatorTests
    {
        [Test]
        public void ShouldReturnSameTypeWhenRequestedShows()
        {
            var ch = ServiceLocator.Instance.GetService<Shows>();
            ch.Should().BeOfType(typeof(Shows));
        }

        [Test]
        public void ShouldReturnSameTypeWhenRequestedShowLibraryService()
        {
            var ch = ServiceLocator.Instance.GetService<ShowLibraryService>();
            ch.Should().BeOfType(typeof(ShowLibraryService));
        }
    }
}
