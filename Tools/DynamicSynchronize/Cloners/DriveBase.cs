using eWolfCommon.Helpers;

namespace DynamicSynchronize.Cloners
{
    public class DriveBase
    {
        protected string _driveLetter;
        protected string _driveName = "Master2";
        private const string _mainData = "MainData";
        private const string _videoStoreMain = "VideoStoreMain";

        public string GetMainDataDriveLetter()
        {
            return $"{FileHelper.GetDriveLetterFor(_mainData)}";
        }

        public string GetVideoStoreMainDriveLetter()
        {
            return $"{FileHelper.GetDriveLetterFor(_videoStoreMain)}";
        }

        protected bool IsDriveConnected()
        {
            _driveLetter = FileHelper.GetDriveLetterFor(_driveName);
            return !string.IsNullOrWhiteSpace(_driveLetter);
        }
    }
}