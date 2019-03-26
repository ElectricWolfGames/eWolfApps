using eWolfPodcasterCore.Data;
using System;

namespace eWolfPodcasterCore.Interfaces
{
    public interface IEpisode
    {
        string Description { get; set; }

        bool Hidden { get; set; }

        PlayedDetails PlayedDetails { get; set; }

        string PodcastURL { get; set; }

        DateTime PublishedDate { get; set; }

        string Show { get; set; }

        string Title { get; set; }
    }
}
