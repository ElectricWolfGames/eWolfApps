using System.IO;

namespace eWolfCommon.Helpers
{
    public static class FileHelper
    {
        public static bool CreateBackUp(string folder, string filename)
        {
            string oldFileName = $"{folder}\\{filename}";

            if (File.Exists(oldFileName))
            {
                string newFileName = $"{folder}\\_backup_{filename}";
                File.Delete(newFileName);
                File.Move(oldFileName, newFileName);
                return true;
            }
            return false;
        }
    }
}