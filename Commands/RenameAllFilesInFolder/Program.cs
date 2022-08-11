using System;
using System.IO;

namespace RenameAllFilesInFolder
{
    internal class Program
    {
        public static void RenameFile(ParseArgs pa, string fileName)
        {
            if (fileName == null)
                return;

            if (fileName.Contains(pa.Replace))
            {
                string newFileName = fileName.Replace(pa.Replace, pa.With);

                Console.WriteLine("Renamed " + Path.GetFileName(fileName) + " With " + Path.GetFileName(newFileName));
                File.Move(fileName, newFileName);
            }
        }

        private static void Main(string[] args)
        {
            ParseArgs pa = new ParseArgs(args);
            if (!pa.IsValid)
            {
                Console.WriteLine(@"Rename All Files In Folder:
Expecting to params in the command line
Old name followed by the new name.
E.G.
    RenameAllFilesInFolder.exe MyOldfileName MyNewName
");
                return;
            }

            string path = Environment.CurrentDirectory;
            string[] files = Directory.GetFiles(path);

            foreach (string fileName in files)
            {
                RenameFile(pa, fileName);
            }
        }
    }
}