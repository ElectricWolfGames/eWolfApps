using eWolfPodcasterCore.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace eWolfPodcasterCore
{
    public class PersistenceHelper<T>
    {
        private readonly string _outputFolder;

        public PersistenceHelper(string outputFolder)
        {
            _outputFolder = outputFolder;
            Directory.CreateDirectory(_outputFolder);
        }

        public ObservableCollection<T> LoadData()
        {
            ObservableCollection<T> items = new ObservableCollection<T>();

            string[] files = Directory.GetFiles(_outputFolder);
            foreach (string file in files)
            {
                Stream stream = null;
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                    T sd = (T)formatter.Deserialize(stream);
                    stream.Close();

                    items.Add(sd);
                }
                catch
                {
                    if (stream != null)
                        stream.Close();
                }
            }

            return items;
        }

        public bool SaveData(IEnumerable<ISaveable> saveableItems)
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
                    if (SaveToStream(stream, formatter, saveable))
                        stream.Close();
                    else
                        allSaved = false;
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
