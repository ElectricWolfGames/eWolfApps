using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace FileDuplicates.Data
{
    public class FileDetails
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public bool Matched { get; set; }

        public int Count
        {
            get
            {
                return Matches.Count;
            }
        }

        private string _hashcode;

        public List<FileDetails> Matches { get; set; } = new List<FileDetails>();

        public string HashCode { get { return _hashcode; } }

        public FileDetails()
        {
        }

        public FileDetails(string fullPath)
        {
            FileName = Path.GetFileName(fullPath);
            FilePath = fullPath.Replace(FileName, string.Empty);
            CreateCode();
            FileDetails fd = new FileDetails();
        }

        public string FullFilePath
        {
            get
            {
                return $"{FilePath}\\{FileName}";
            }
        }

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