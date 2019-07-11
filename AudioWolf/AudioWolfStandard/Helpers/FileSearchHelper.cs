using AudioWolfStandard.Options;
using AudioWolfStandard.Services;

namespace AudioWolfStandard.Helpers
{
    public static class FileSearchHelper
    {
        public static List<string> GetAllFiles()
        {
            OptionsHolder optionsHolder = ServiceLocator.Instance.GetService<OptionsHolder>();
            string serach = "*.mp3";
            List<string> files = new List<string>();

            foreach (string path in optionsHolder.PathsToSearch)
            {
                var filesRaw = Directory.GetFiles(path, serach, SearchOption.AllDirectories);
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
