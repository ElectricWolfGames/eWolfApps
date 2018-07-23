using System;

namespace eWolfPodcasterCore.Interfaces
{
    public interface IPodCastInfo
    {
        string Description { get; }

        IPodCastInfo EpisodeData { get; set; }

        long PlayedLength { get; set; }

        double PlayedLengthScaled { get; set; }

        string PodcastURL { get; }

        DateTime PublishedDate { get; }

        string Title { get; }

        void DownloadAsMp3();

        string GetOffLineFileName();

        bool IsOffLine();
    }
}
