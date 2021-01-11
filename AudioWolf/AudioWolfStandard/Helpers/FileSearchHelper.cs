using AudioWolfStandard.Options;
using AudioWolfStandard.Services;
using System.Collections.Generic;
using System.IO;

namespace AudioWolfStandard.Helpers
{
    public static class FileSearchHelper
    {
        public static List<string> GetAllFiles()
        {
            OptionsHolder optionsHolder = ServiceLocator.Instance.GetService<OptionsHolder>();
            List<string> files = new List<string>();

            foreach (string path in optionsHolder.PathsToSearch)
            {
                string serach = "*.mp3";
                var filesRaw = Directory.GetFiles(path, serach, SearchOption.AllDirectories);
                foreach (string filename in filesRaw)
                {
                    if (string.IsNullOrWhiteSpace(filename))
                        continue;

                    files.Add(filename);
                }

                serach = "*.wav";
                filesRaw = Directory.GetFiles(path, serach, SearchOption.AllDirectories);
                foreach (string filename in filesRaw)
                {
                    if (string.IsNullOrWhiteSpace(filename))
                        continue;

                    files.Add(filename);
                }
            }

            return files;
        }
    }
}