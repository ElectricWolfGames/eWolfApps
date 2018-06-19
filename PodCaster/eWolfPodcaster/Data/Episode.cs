using System;

namespace eWolfPodcaster.Data
{
    public class Episode
    {
        public string Description { get; set; }

        public bool Hidden { get; set; }

        public PlayedDetails PlayedDetails { get; set; } = new PlayedDetails();

        public string PodcastURL { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Show { get; set; }

        public string Title { get; set; }
    }
}
