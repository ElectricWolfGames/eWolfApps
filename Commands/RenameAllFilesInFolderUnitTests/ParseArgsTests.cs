using FluentAssertions;
using NUnit.Framework;
using RenameAllFilesInFolder;

namespace RenameAllFilesInFolderUnitTests
{
    public class ParseArgsTests
    {
        [Test]
        public void ShouldNotParseOneArgs()
        {
            ParseArgs pa = new ParseArgs(new string[] { "from" });
            pa.IsValid.Should().BeFalse();
        }

        [Test]
        public void ShouldNotParseThreeArgs()
        {
            ParseArgs pa = new ParseArgs(new string[] { "from", "To", "Somewhere" });
            pa.IsValid.Should().BeFalse();
        }

        [Test]
        public void ShouldParseArgs()
        {
            ParseArgs pa = new ParseArgs(new string[] { "from", "to" });
            pa.IsValid.Should().BeTrue();
            pa.Replace.Should().Be("from");
            pa.With.Should().Be("to");
        }
    }
}