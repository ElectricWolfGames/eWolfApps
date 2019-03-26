using eWolfPodcasterCore.Data;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace eWolfPodcasterUI.UserControls
{
    public partial class PodcastEpisode : UserControl
    {
        private EpisodeControl _episodeData;

        public PodcastEpisode()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Description
        {
            get
            {
                return _episodeData.Description;
            }
        }

        public EpisodeControl EpisodeControlData
        {
            get
            {
                return _episodeData;
            }

            set
            {
                _episodeData = value;
                _showplayed.Height = _episodeData.PlayedLengthScaled;
            }
        }

        public bool IsOffLine
        {
            get
            {
                return EpisodeControlData.IsOffLine;
            }
        }

        public string IsOffLineDisplay
        {
            get
            {
                Console.WriteLine("IsOffLineDisplay : " + IsOffLine);
                return IsOffLine ? "-" : "D";
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
                _showplayed.Height = value;
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
                return EpisodeControlData.PublishedDate;
            }
        }

        public string ShowName
        {
            get
            {
                return _episodeData.ShowName;
            }
            set
            {
                _episodeData.ShowName = value;
            }
        }

        public string Title
        {
            get
            {
                return _episodeData.Title;
            }
        }

        public string UrlToPlay
        {
            get
            {
                if (IsOffLine)
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
            return EpisodeControlData.GetOffLineFileName();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void _butRemove_Click(object sender, RoutedEventArgs e)
        {
            // TODO: remove the eposode from the list
        }

        private void _buttonDownloadShow_Click(object sender, RoutedEventArgs e)
        {
            _episodeData.DownloadAsMp3();
        }
    }
}
