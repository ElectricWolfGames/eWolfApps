using eWolfCommon.Interfaces;
using eWolfCommon.Helpers;
using FileDuplicates.Data;
using System;
using System.Collections.Generic;

namespace FileDuplicates.Services
{
    [Serializable]
    public class FileDetailsHolderService : ISaveable
    {
        public List<FileDetails> Details = new List<FileDetails>();

        public FileDetailsHolderService()
        {
        }

        public string GetFileName
        {
            get
            {
                return "eWolfFileDuplicates.dat";
            }
        }

        public static void Save(FileDetailsHolderService fileDetailsHolderService, string path)
        {
            lock (fileDetailsHolderService.Details)
            {
                PersistenceHelper<FileDetailsHolderService> ph = new PersistenceHelper<FileDetailsHolderService>(path);
                ph.SaveDataSingle(fileDetailsHolderService);
            }
        }

        internal void AddFrom(FileDetailsHolderService fileDetailsHolderService)
        {
            if (fileDetailsHolderService == null)
            {
                return;
            }

            Details.Clear();
            foreach (var f in fileDetailsHolderService.Details)
            {
                Details.Add(f);
            }
        }

        public static FileDetailsHolderService Load(string path)
        {
            PersistenceHelper<FileDetailsHolderService> ph = new PersistenceHelper<FileDetailsHolderService>(path);
            FileDetailsHolderService tf = ph.LoadDataSingle(path + @"\eWolfFileDuplicates.dat");

            return tf;
        }
    }
}