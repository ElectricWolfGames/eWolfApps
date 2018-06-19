using System.Collections.Generic;

namespace eWolfPodcaster.Data
{
    public class Show
    {
        public string Category { get; set; }

        public bool CheckforUpdates { get; set; }

        public bool IncludeSubFolders { get; set; }

        public string RssFeed { get; set; }

        public ShowStorageType ShowStorage { get; set; }

        public string Title { get; set; }

        public ICollection<Episode> Episodes { get; set; }
    }
}
