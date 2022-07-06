using DynamicSynchronize.Cloners;

namespace SynchronizeFolders
{
    public class Program
    {
        private const string MainData = "MainData";
        private const string Master2 = "Master2";
        private const string MasterBackup = "MasterBackup";
        private const string VideoStoreMain = "VideoStoreMain";

        private static void Main(string[] args)
        {
            Console.WriteLine("Synchronize Folders");

            var drive_Master = new Drive_Master2();
            drive_Master.Process();

            var MasterBackup = new MasterBackup();
            MasterBackup.Process();

            //string BackUpA = FileHelper.GetDriveLetterFor(MasterBackup);
            //string master2 = FileHelper.GetDriveLetterFor(Master2);
            //
            //Console.WriteLine(drive);
            //Console.WriteLine(BackUpA);
            //GetDrives();

            // VideoStoreMain:\Films\1930\
            // MasterBackup\Films\1930\
        }
    }
}