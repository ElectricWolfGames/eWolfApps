using eWolfPodcasterCore;
using eWolfPodcasterCore.Interfaces;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            PersistenceHelper<ISaveable> ph = new PersistenceHelper<ISaveable>(GetOutputFolder());
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
            PersistenceHelper<ISaveable> ph = new PersistenceHelper<ISaveable>(GetOutputFolder());
            bool results = ph.SaveData(new List<ISaveable>() { tempSaveable, tempSaveableB });

            results.Should().BeTrue();
            string finalName = Path.Combine(GetOutputFolder(), "TestSave.data");
            File.Exists(finalName).Should().BeTrue();

            finalName = Path.Combine(GetOutputFolder(), "TestSave2.data");
            File.Exists(finalName).Should().BeTrue();
        }

        [Test]
        public void ShouldLoadSavedData()
        {
            RemoveTempFolder();

            TempSaveable tempSaveable = new TempSaveable();
            tempSaveable.Name = "MyName";
            tempSaveable.OtherData = "OtherData";
            PersistenceHelper<TempSaveable> ph = new PersistenceHelper<TempSaveable>(GetOutputFolder());
            ph.SaveData(new List<ISaveable>() { tempSaveable });

            ObservableCollection<TempSaveable> loadItems = ph.LoadData();
            loadItems.Should().HaveCount(1);
            loadItems[0].Name.Should().Be("MyName");
            loadItems[0].OtherData.Should().Be("OtherData");
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

        [Serializable]
        public class TempSaveable : ISaveable
        {
            public string GetFileName => "TestSave.data";

            public string Name { get; set; }

            public string OtherData { get; set; }
        }

        [Serializable]
        public class TempSaveableAnotherName : ISaveable
        {
            public string GetFileName => "TestSave2.data";
        }
    }
}
