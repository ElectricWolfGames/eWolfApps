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
        public void ShouldSaveOneFile()
        {
            RemoveTempFolder();

            TempSaveable tempSaveable = new TempSaveable();
            PersistenceHelper ph = new PersistenceHelper(GetOutputFolder());
            ph.SaveData(new List<ISaveable>() { tempSaveable });

            string finalName = Path.Combine(GetOutputFolder(), "TestSave.data");
            File.Exists(finalName).Should().BeTrue();
        }

        [Test]
        public void ShouldSaveTwoFile()
        {
            RemoveTempFolder();

            TempSaveable tempSaveable = new TempSaveable();
            TempSaveableAnotherName tempSaveableB = new TempSaveableAnotherName();
            PersistenceHelper ph = new PersistenceHelper(GetOutputFolder());
            bool results = ph.SaveData(new List<ISaveable>() { tempSaveable, tempSaveableB });

            results.Should().BeTrue();
            string finalName = Path.Combine(GetOutputFolder(), "TestSave.data");
            File.Exists(finalName).Should().BeTrue();

            finalName = Path.Combine(GetOutputFolder(), "TestSave2.data");
            File.Exists(finalName).Should().BeTrue();
        }

        [TearDown]
        public void TearDown()
        {
            RemoveTempFolder();
        }

        private string GetOutputFolder()
        {
            return Path.Combine(Path.GetTempPath(), "eWolfTests");
        }

        private void RemoveTempFolder()
        {
            try
            {
                Directory.Delete(GetOutputFolder(), true);
            }
            catch
            {
            }
        }

        public class TempSaveable : ISaveable
        {
            public string GetFileName => "TestSave.data";
        }

        public class TempSaveableAnotherName : ISaveable
        {
            public string GetFileName => "TestSave2.data";
        }
    }
}
