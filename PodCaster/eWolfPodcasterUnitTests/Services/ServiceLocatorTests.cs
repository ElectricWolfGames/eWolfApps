using eWolfPodcasterCore.Services;
using FluentAssertions;
using NUnit.Framework;

namespace eWolfPodcasterCoreUnitTests.Services
{
    public class ServiceLocatorTests
    {
        [Test]
        public void Should()
        {
            var ch = ServiceLocator.Instance.GetService<CategoryHolderService>();
            ch.Should().BeOfType(typeof(CategoryHolderService));
        }
    }
}
