using System;
using System.Threading;

namespace SynchronizeFolders
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Synchronize Folders");

            if (args.Length == 2)
            {
                ProcessFiles(args);
            }
            else
            {
                Console.WriteLine("We need to have two args");
                Console.WriteLine("The from path");
                Console.WriteLine("The to path");
                if (args.Length > 0)
                    Console.WriteLine(args[0]);
                if (args.Length > 1)
                    Console.WriteLine(args[1]);
                if (args.Length > 2)
                    Console.WriteLine(args[2]);

                Thread.Sleep(10000);
            }
        }

        private static void ProcessFiles(string[] args)
        {
            string from = args[0];
            string to = args[1];

            Console.WriteLine($"from {from}");
            Console.WriteLine($"to {to}");

            Thread.Sleep(2000);

            try
            {
                SynchronizeFoldersProcesseor synchronizeFolders = new SynchronizeFoldersProcesseor();
                synchronizeFolders.Now(from, to);
            }
            catch
            {
                Thread.Sleep(5000);
            }
        }
    }
}