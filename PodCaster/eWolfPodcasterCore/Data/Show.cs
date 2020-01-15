using eWolfPodcasterCore.Library;
using System;
using System.Collections.Generic;

namespace eWolfPodcasterCore.Data
{
    [Serializable]
    public class Show
    {
        public List<EpisodeControl> Episodes { get; set; } = new List<EpisodeControl>();

        public string RssFeed { get; set; }

        public ShowOptions ShowOption { get; set; } = new ShowOptions();

        public string Title { get; set; }

        public CatergeryData Catergery { get; set; } = new CatergeryData("None");

        public int Count
        {
            get
            {
                return Episodes.Count;
            }
        }
    }
}