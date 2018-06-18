using FluentAssertions;
using NUnit.Framework;

namespace eWolfPodcasterUnitTests
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            int i = 0;
            i++;
            i.Should().Be(1);
        }
    }
}
