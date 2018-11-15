using AudioWolfStandard;
using AudioWolfStandard.Data;
using FluentAssertions;
using NUnit.Framework;

namespace AudioWolfBaseUnitTests
{
    public class SoundHolderTests
    {
        [Test]
        public void ShouldAddItemToList()
        {
            SoundHolder sh = new SoundHolder();
            SoundItemData sid = new SoundItemData
            {
                Name = "Added item"
            };
            sh.Add(sid);

            sh.SoundItems.Should().HaveCount(1);
            sh.SoundItems[0].Name.Should().Be("Added item");
        }

        [Test]
        public void ShouldNotHaveSameItemTwice()
        {
            SoundHolder sh = new SoundHolder();
            SoundItemData sid = new SoundItemData
            {
                Name = "Added item",
                FullPath = @"C:\FullPath\Wav.wav"
            };
            sh.Add(sid);

            SoundItemData sidTwo = new SoundItemData
            {
                Name = "Added item Different name",
                FullPath = @"C:\FullPath\Wav.wav"
            };
            sh.Add(sidTwo);

            sh.SoundItems.Should().HaveCount(1);
            sh.SoundItems[0].Name.Should().Be("Added item");
        }
    }
}
