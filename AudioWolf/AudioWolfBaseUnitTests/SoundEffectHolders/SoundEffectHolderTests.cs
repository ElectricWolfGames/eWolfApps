using AudioWolfStandard;
using AudioWolfStandard.Data;
using FluentAssertions;
using NUnit.Framework;

namespace AudioWolfBaseUnitTests.SoundEffectHolders
{
    public class SoundEffectHolderTests
    {
        [TestCase("Name", "Name")]
        [TestCase("Name Other 12", "Name Other 12")]
        [TestCase("Name  Space", "Name Space")]
        [TestCase("Name Name Space", "Name Space")]
        [TestCase("name Name space", "Name Space")]
        public void ShouldFixNames(string name, string fixedName)
        {
            SoundEffectHolder.FixName(name).Should().Be(fixedName);
        }

        [Test]
        public void ShouldFixAllNames()
        {
            SoundEffectHolder seh = new SoundEffectHolder();
            SoundDetails sd = new SoundDetails();
            sd.FullPath = @"c:\Somewhere\Name Name    Other.mp3";
            seh.Add(sd);

            seh.FixNames();
            seh.Sounds[0].Name.Should().Be("Name Other");
        }
    }
}