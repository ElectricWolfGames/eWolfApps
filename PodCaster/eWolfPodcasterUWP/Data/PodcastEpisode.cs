using eWolfPodcasterCore.Interfaces;
using System;

namespace eWolfPodcasterUWP.Data
{
    public class PodcastEpisode : IPodCastInfo
    {
        public PodcastEpisode()
        {
        }

        public string Description
        {
            get
            {
                return EpisodeData.Description;
            }
        }

        public IPodCastInfo EpisodeData { get; set; }

        public long PlayedLength
        {
            get { return EpisodeData.PlayedLength; }
            set { EpisodeData.PlayedLength = value; }
        }

        public double PlayedLengthScaled
        {
            get
            {
                return EpisodeData.PlayedLengthScaled;
            }

            set
            {
                EpisodeData.PlayedLengthScaled = value;
            }
        }

        public string PodCastDownloadable
        {
            get
            {
                if (EpisodeData.IsOffLine())
                    return "-";
                else
                    return "D";
            }
        }

        public string PodCastTime
        {
            get { return EpisodeData.PublishedDate.ToLongDateString(); }
        }

        public string PodcastURL
        {
            get { return EpisodeData.PodcastURL; }
        }

        public DateTime PublishedDate
        {
            get
            {
                return EpisodeData.PublishedDate;
            }
        }

        public string Title
        {
            get
            {
                return EpisodeData.Title;
            }
        }

        public string UrlToPlay
        {
            get
            {
                if (IsOffLine())
                {
                    return GetOffLineFileName();
                }
                return PodcastURL.Replace("https", "http");
            }
        }

        public void DownloadAsMp3()
        {
            throw new NotImplementedException();
        }

        public string GetOffLineFileName()
        {
            return EpisodeData.GetOffLineFileName();
        }

        public bool IsOffLine()
        {
            return EpisodeData.IsOffLine();
        }
    }
}
