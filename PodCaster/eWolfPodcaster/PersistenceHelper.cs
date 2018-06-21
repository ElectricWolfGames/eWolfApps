using eWolfPodcaster.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace eWolfPodcaster
{
    public class PersistenceHelper
    {
        private PersistenceStorage _persistenceStore = PersistenceStorage.File;
        private string _outputFolder;

        public PersistenceHelper(string outputFolder)
        {
            _outputFolder = outputFolder;
            Directory.CreateDirectory(_outputFolder);
        }

        public void LoadData()
        {
        }

        public void SaveData(List<ISaveable> saveableItems)
        {
            foreach (ISaveable saveable in saveableItems)
            {
                string outputFileName = Path.Combine(_outputFolder, saveable.GetFileName);
                IFormatter formatter = new BinaryFormatter();
                try
                {
                    Stream stream = StreamFactory.GetStream(outputFileName, _persistenceStore);
                    SaveToStream(stream, formatter, saveable);
                }
                catch
                {
                }
            }
        }

        public static bool SaveToStream(Stream stream, IFormatter formatter, object objectToSave)
        {
            try
            {
                formatter.Serialize(stream, objectToSave);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public static class StreamFactory
    {
        public static Stream GetStream(string outputName, PersistenceStorage store)
        {
            Stream stream = null;
            if (store == PersistenceStorage.File)
            {
                stream = new FileStream(outputName, FileMode.Create, FileAccess.Write, FileShare.None);
            }
            if (store == PersistenceStorage.Memory)
            {
                stream = new MemoryStream();
            }

            return stream;
        }
    }
}
