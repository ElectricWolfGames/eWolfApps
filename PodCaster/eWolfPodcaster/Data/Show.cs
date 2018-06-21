using System;
using System.Collections.Generic;

namespace eWolfPodcaster.Data
{
    [Serializable]
    public class Show
    {
        public ICollection<Episode> Episodes { get; set; }

        public string RssFeed { get; set; }

        public ShowOptions ShowOption { get; set; } = new ShowOptions();

        public string Title { get; set; }
    }
}
