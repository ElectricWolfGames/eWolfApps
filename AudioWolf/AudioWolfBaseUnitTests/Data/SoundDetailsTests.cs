using AudioWolfStandard.Data;
using FluentAssertions;
using NUnit.Framework;

namespace AudioWolfBaseUnitTests.Data
{
    public class SoundDetailsTests
    {
        [Test]
        public void ShouldSetModifyedWhenChanged()
        {
            SoundDetails sd = new SoundDetails();
            sd.FullPath = @"c:\Somewhere\MyName.mp3";
            sd.Name = "NewName";
            sd.IsModified.Should().BeTrue();
        }

        [Test]
        public void ShouldNowSetModifyedWhenSame()
        {
            SoundDetails sd = new SoundDetails();
            sd.FullPath = @"c:\Somewhere\MyName.mp3";
            sd.Name = "MyName";
            sd.IsModified.Should().BeFalse();
        }
    }
}
