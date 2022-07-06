using SynchronizeFolders;

namespace DynamicSynchronize.Cloners
{
    public class MasterBackup : DriveBase
    {
        public MasterBackup()
        {
            _driveName = "MasterBackup";
        }

        public void Process()
        {
            if (!IsDriveConnected())
                return;

            Console.WriteLine("Connected!");

            CloneFilms();
            FilmsClassicSciFi();
        }

        private void CloneFilms()
        {
            string pathTo = @$"{_driveLetter}Films\";
            string pathFrom = @$"{GetVideoStoreMainDriveLetter()}\Films\";

            Console.WriteLine($"CLONE: Films from {pathFrom} to {pathTo}");

            var synchronizeFolders = new SynchronizeFoldersProcesseor();
            synchronizeFolders.Now(pathFrom, pathTo);
        }

        private void FilmsClassicSciFi()
        {
            string pathTo = @$"{_driveLetter}FilmsClassicSci-Fi\";
            string pathFrom = @$"{GetVideoStoreMainDriveLetter()}\FilmsClassicSci-Fi\";

            Console.WriteLine($"CLONE: FilmsClassicSci-Fi from {pathFrom} to {pathTo}");

            var synchronizeFolders = new SynchronizeFoldersProcesseor();
            synchronizeFolders.Now(pathFrom, pathTo);
        }

        private void FilmsTV()
        {
            string pathTo = @$"{_driveLetter}TV\";
            string pathFrom = @$"{GetVideoStoreMainDriveLetter()}\TV\";

            Console.WriteLine($"CLONE: FilmsClassicSci-Fi from {pathFrom} to {pathTo}");

            var synchronizeFolders = new SynchronizeFoldersProcesseor();
            synchronizeFolders.Now(pathFrom, pathTo);
        }
    }
}