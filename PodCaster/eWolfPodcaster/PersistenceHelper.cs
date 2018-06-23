using eWolfPodcaster.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace eWolfPodcaster
{
    public class PersistenceHelper
    {
        private string _outputFolder;

        public PersistenceHelper(string outputFolder)
        {
            _outputFolder = outputFolder;
            Directory.CreateDirectory(_outputFolder);
        }

        public void LoadData()
        {
        }

        public bool SaveData(List<ISaveable> saveableItems)
        {
            bool allSaved = true;
            foreach (ISaveable saveable in saveableItems)
            {
                string outputFileName = Path.Combine(_outputFolder, saveable.GetFileName);
                IFormatter formatter = new BinaryFormatter();
                Stream stream = null;
                try
                {
                    stream = StreamFactory.GetStream(outputFileName);
                    SaveToStream(stream, formatter, saveable);
                    stream.Close();
                }
                catch
                {
                    if (stream != null)
                        stream.Close();

                    allSaved = false;
                }
            }
            return allSaved;
        }

        private static bool SaveToStream(Stream stream, IFormatter formatter, object objectToSave)
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
}
