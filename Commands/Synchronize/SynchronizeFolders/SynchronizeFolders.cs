using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SynchronizeFolders
{
    public class SynchronizeFoldersProcesseor
    {
        public void Now(string from, string to)
        {
            string[] files = Directory.GetFiles(from, "", SearchOption.AllDirectories);

            List<string> exculedFile = new List<string>();

            RemoveFromBackUp(from, to);

            int count = 0;
            int total = files.Length;
            foreach (var file in files)
            {
                if (exculedFile.Contains(file))
                    continue;

                if (file.Contains("_STORE"))
                    continue;

                string partFile = file.Replace(from, string.Empty);
                string dest = to + partFile;

                if (!File.Exists(dest))
                {
                    Console.WriteLine($" Files {count++} of {total} : COPY {file}");
                    Thread.Sleep(1000);

                    Directory.CreateDirectory(Path.GetDirectoryName(dest));
                    File.Copy(file, dest);
                }
                else
                {
                    //Console.WriteLine($" Files {count++} of {total} : SAME {file}");
                    DateTime fileDetailsFrom = File.GetLastWriteTime(file);
                    DateTime fileDetailsTo = File.GetLastWriteTime(dest);

                    if (fileDetailsFrom.Ticks > fileDetailsTo.Ticks)
                    {
                        File.Copy(file, dest, true);
                    }
                }
            }

            Console.WriteLine($"DONE!");
        }

        private void RemoveFromBackUp(string from, string to)
        {
            string[] filesTo = Directory.GetFiles(to, "", SearchOption.AllDirectories);
            foreach (var file in filesTo)
            {
                if (file.Contains("_STORE"))
                    continue;

                string partFile = file.Replace(to, string.Empty);
                string dest = from + partFile;
                if (!File.Exists(dest))
                {
                    File.Delete(file);
                }
            }
        }
    }
}