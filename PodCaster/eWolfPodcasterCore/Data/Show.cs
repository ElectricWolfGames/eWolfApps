using System;
using System.Collections.Generic;

namespace eWolfPodcasterCore.Data
{
    [Serializable]
    public class Show
    {
        public List<Episode> Episodes { get; set; } = new List<Episode>();

        public string RssFeed { get; set; }

        public ShowOptions ShowOption { get; set; } = new ShowOptions();

        public string Title { get; set; }
    }
}
