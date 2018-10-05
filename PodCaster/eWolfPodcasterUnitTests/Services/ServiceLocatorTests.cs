using eWolfPodcasterCore.Services;
using FluentAssertions;
using NUnit.Framework;

namespace eWolfPodcasterCoreUnitTests.Services
{
    public class ServiceLocatorTests
    {
        [Test]
        public void ShouldReturnSameTypeWhenRequestedCategoryHolderService()
        {
            var ch = ServiceLocator.Instance.GetService<CategoryHolderService>();
            ch.Should().BeOfType(typeof(CategoryHolderService));
        }

        [Test]
        public void ShouldReturnSameTypeWhenRequestedShowLibraryService()
        {
            var ch = ServiceLocator.Instance.GetService<ShowLibraryService>();
            ch.Should().BeOfType(typeof(ShowLibraryService));
        }
    }
}
