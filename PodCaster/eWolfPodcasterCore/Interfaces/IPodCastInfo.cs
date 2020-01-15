using System;

namespace eWolfPodcasterCore.Interfaces
{
    public interface IPodCastInfo
    {
        string Description { get; }

        IEpisode EpisodeData { get; set; }

        bool IsOffLine { get; }
        long PlayedLength { get; set; }

        double PlayedLengthScaled { get; set; }

        string PodcastURL { get; }

        DateTime PublishedDate { get; }

        string ShowName { get; set; }
        string Title { get; }

        void DownloadAsMp3();

        string GetOffLineFileName();
    }
}