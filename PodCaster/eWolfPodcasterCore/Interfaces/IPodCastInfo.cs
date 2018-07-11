using System;

namespace eWolfPodcasterCore.Interfaces
{
    public interface IPodCastInfo
    {
        string Title { get; }

        string Description { get; }

        string PodcastURL { get; }

        IPodCastInfo EpisodeData { get; set; }

        long PlayedLength { get; set; }

        double PlayedLengthScaled { get; set; }

        bool IsOffLine();

        string GetOffLineFileName();

        void DownloadAsMp3();

        DateTime PublishedDate { get; }
    }
}
