using AudioWolfStandard.Data;
using AudioWolfStandard.Helpers;
using AudioWolfStandard.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AudioWolfStandard
{
    [Serializable]
    public class SoundHolder : ISaveable
    {
        private bool _modified = false;

        public List<SoundItemData> SoundItems = new List<SoundItemData>();

        // need to have many collection - not just the one
        private readonly string _outputFolder;

        public SoundHolder()
        {
        }

        public SoundHolder(string outputFolder)
        {
            _outputFolder = outputFolder;
            Load();
        }

        public string GetFileName
        {
            get
            {
                return "SoundItemsHolderA";
            }
        }

        public void Add(SoundItemData item)
        {
            if (SoundItems.Any(x => x.FullPath == item.FullPath))
                return;

            _modified = true;
            lock (SoundItems)
            {
                SoundItems.Add(item);
            }
        }

        public void Load()
        {
            PersistenceHelper<SoundHolder> ph = new PersistenceHelper<SoundHolder>(_outputFolder);

            string outputFileName = Path.Combine(_outputFolder, GetFileName);
            SoundHolder tempSoundHolder = ph.LoadDataSingle(outputFileName);

            SoundItems.Clear();
            if (tempSoundHolder != null)
            {
                SoundItems.AddRange(tempSoundHolder.SoundItems);
            }
        }

        public void SaveIfNeeded()
        {
            if (_modified)
                Save();
        }

        private void Save()
        {
            lock (SoundItems)
            {
                PersistenceHelper<SoundHolder> ph = new PersistenceHelper<SoundHolder>(_outputFolder);
                ph.SaveDataSingle(this);
            }
        }
    }
}
