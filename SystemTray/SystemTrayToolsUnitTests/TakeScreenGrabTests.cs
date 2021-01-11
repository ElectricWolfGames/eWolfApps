using FluentAssertions;
using NUnit.Framework;
using SystemTrayTools.Actions;

namespace SystemTrayToolsUnitTests
{
    public class TakeScreenGrabTests
    {
        [Test]
        public void ShouldGetOrder()
        {
            TakeScreenGrab tsg = new TakeScreenGrab();
            tsg.OrderIndex.Should().Be(10);
        }
    }
}