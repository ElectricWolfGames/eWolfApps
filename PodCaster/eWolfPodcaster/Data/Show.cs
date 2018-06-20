using System.Collections.Generic;

namespace eWolfPodcaster.Data
{
    public class Show
    {
        public ICollection<Episode> Episodes { get; set; }

        public string RssFeed { get; set; }

        public ShowOptions ShowOption { get; set; } = new ShowOptions();

        public string Title { get; set; }
    }
}
