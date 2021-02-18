using System;

namespace eWolfPodcasterCore.Data
{
    [Serializable]
    public class ShowOptions
    {
        public bool AudoDownloadEpisodes { get; set; }

        public string Category { get; set; }

        public bool CheckforUpdates { get; set; } = true;

        public bool IncludeSubFolders { get; set; }

        public ShowStorageType ShowStorage { get; set; }

        public bool Starred { get; set; }

        // public UpdatedFrequency updatedFrequency { get; set; } = UpdatedFrequency.Weekly;
    }
}