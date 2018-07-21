using eWolfPodcasterCore.Interfaces;
using System;

namespace eWolfPodcasterUWP
{
    public class PodcastEpisode : IPodCastInfo
    {
        private IPodCastInfo _episodeData;

        public PodcastEpisode()
        {
        }

        public string Description
        {
            get
            {
                return _episodeData.Description;
            }
        }

        public IPodCastInfo EpisodeData
        {
            get
            {
                return _episodeData;
            }

            set
            {
                _episodeData = value;
            }
        }

        public long PlayedLength
        {
            get { return _episodeData.PlayedLength; }
            set { _episodeData.PlayedLength = value; }
        }

        public double PlayedLengthScaled
        {
            get
            {
                return _episodeData.PlayedLengthScaled;
            }

            set
            {
                _episodeData.PlayedLengthScaled = value;
            }
        }

        public string PodCastDownloadable
        {
            get
            {
                if (_episodeData.IsOffLine())
                    return "-";
                else
                    return "D";
            }
        }

        public string PodCastTime
        {
            get { return _episodeData.PublishedDate.ToLongDateString(); }
        }

        public string PodcastURL
        {
            get { return _episodeData.PodcastURL; }
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
                return _episodeData.Title;
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
