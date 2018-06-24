using System;

namespace eWolfPodcaster.Data
{
    [Serializable]
    public class ShowOptions
    {
        public bool AudoDownloadEpisodes { get; set; }

        public string Category { get; set; }

        public bool CheckforUpdates { get; set; } = true;

        public bool IncludeSubFolders { get; set; }

        public ShowStorageType ShowStorage { get; set; }
    }
}
