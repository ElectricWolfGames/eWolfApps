using System;
using System.IO;

namespace eWolfCommon.Helpers
{
    public static class FileHelper
    {
        public static bool CreateBackFileDate(string folder, string filename)
        {
            string oldFileName = $"{folder}\\{filename}";

            if (File.Exists(oldFileName))
            {
                Directory.CreateDirectory($"{folder}\\back\\");

                string newFileName = $"{folder}\\back\\{DateTime.Now.ToString("yyyyMMdd")}_{filename}";
                File.Delete(newFileName);
                File.Move(oldFileName, newFileName);
                return true;
            }
            return false;
        }

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

        public static string GetDriveLetterFor(string name)
        {
            name = name.ToLower();

            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (var drive in drives)
            {
                if (drive.IsReady)
                {
                    if (drive.VolumeLabel.ToLower() == name)
                    {
                        return drive.Name;
                    }
                }
            }
            return null;
        }
    }
}