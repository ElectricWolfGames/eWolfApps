using SynchronizeFolders;

namespace DynamicSynchronize.Cloners
{
    public class Drive_Master2 : DriveBase
    {
        public Drive_Master2()
        {
            _driveName = "Master2";
        }

        public void Process()
        {
            if (!IsDriveConnected())
                return;

            Console.WriteLine("Connected!");

            CloneBackUpZips();
            CloneTrains();
            CloneTextures();
            CloneVintageSciFi();
        }

        private void CloneBackUpZips()
        {
            string pathTo = @$"{_driveLetter}_BackUpZips\";
            string pathFrom = @$"{GetMainDataDriveLetter()}_BackUpZips\";

            Console.WriteLine($"CLONE: BackUpZip from {pathFrom} to {pathTo}");

            var synchronizeFolders = new SynchronizeFoldersProcesseor();
            synchronizeFolders.Now(pathFrom, pathTo);
        }

        private void CloneTextures()
        {
            string pathTo = @$"{_driveLetter}Development\Textures\";
            string pathFrom = @$"{GetMainDataDriveLetter()}Textures\";

            Console.WriteLine($"CLONE: Textures from {pathFrom} to {pathTo}");

            var synchronizeFolders = new SynchronizeFoldersProcesseor();
            synchronizeFolders.Now(pathFrom, pathTo);
        }

        private void CloneTrains()
        {
            string pathTo = @$"{_driveLetter}Development\Trains\";
            string pathFrom = @$"{GetMainDataDriveLetter()}Trains\";

            Console.WriteLine($"CLONE: Trains from {pathFrom} to {pathTo}");

            var synchronizeFolders = new SynchronizeFoldersProcesseor();
            synchronizeFolders.Now(pathFrom, pathTo);
        }

        private void CloneVintageSciFi()
        {
            string pathTo = @$"{_driveLetter}Development\VintageSciFi\";
            string pathFrom = @$"{GetMainDataDriveLetter()}VintageFilms\";

            Console.WriteLine($"CLONE: VintageFilms from {pathFrom} to {pathTo}");

            var synchronizeFolders = new SynchronizeFoldersProcesseor();
            synchronizeFolders.Now(pathFrom, pathTo);
        }
    }
}