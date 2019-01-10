using System;
using System.IO;

namespace Increment
{
    public class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                string filename = args[0];
                string key = args[1];
                string text = File.ReadAllText(filename);
                string updatedData = IncrementNumbers.Process(key, text);
                File.WriteAllText(filename, updatedData);
                Console.WriteLine();

                string updateFileName = IncrementNumbers.Process(key, filename);
                Console.WriteLine();
                File.Move(filename, updateFileName);
                Console.WriteLine($"Saving file as {updateFileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
