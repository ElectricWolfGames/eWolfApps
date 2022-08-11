using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace FileDuplicates.Data
{
    [Serializable]
    public class FileDetails
    {
        private string _hashcode;

        public FileDetails()
        {
        }

        public FileDetails(string fullPath)
        {
            FileName = Path.GetFileName(fullPath);
            FilePath = fullPath.Replace(FileName, string.Empty);
            CreateCode();
        }

        public int Count
        {
            get
            {
                return Matches.Count;
            }
        }

        public string FileName { get; set; }
        public string FilePath { get; set; }

        public string FullFilePath
        {
            get
            {
                return $"{FilePath}{FileName}";
            }
        }

        public string HashCode
        { get { return _hashcode; } }
        public bool Matched { get; set; }

        [System.Runtime.Serialization.IgnoreDataMemberAttribute]
        public List<FileDetails> Matches { get; set; } = new List<FileDetails>();

        public void CreateCode()
        {
            using (MD5 md5 = MD5.Create())
            {
                using (FileStream stream = File.OpenRead(FullFilePath))
                {
                    byte[] hash = md5.ComputeHash(stream);
                    _hashcode = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}