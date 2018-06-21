using eWolfPodcaster;
using eWolfPodcaster.Interfaces;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace eWolfPodcasterUnitTests
{
    public class PersistenceHelperTests
    {
        [Test]
        public void ShouldSaveFile()
        {
            TempSaveable sc = new TempSaveable();

            try
            {
                Directory.Delete(GetOutputFolder());
            }
            catch { }

            PersistenceHelper ph = new PersistenceHelper(GetOutputFolder());
            ph.SaveData(new List<ISaveable>() { sc });

            string finalName = Path.Combine(GetOutputFolder(), "TestSave.data");
            File.Exists(finalName).Should().BeTrue();
        }

        public class TempSaveable : ISaveable
        {
            public string GetFileName => "TestSave.data";
        }

        public string GetOutputFolder()
        {
            return Path.Combine(Path.GetTempPath(), "eWolfTests");
        }
    }
}
