using eWolfPodcasterCore.Interfaces;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace eWolfPodcasterUI.CustomDialog
{
    public partial class PodcastEpisode : UserControl, IPodCastInfo
    {
        private IPodCastInfo _episodeData;

        public PodcastEpisode()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Description
        {
            get { return _episodeData.Description; }
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
                _showplayed.Height = _episodeData.PlayedLengthScaled;
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
            get { return _episodeData.Title; }
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
            // remove episode
        }

        /// <summary>
        /// User has click on the download button
        /// </summary>
        /// <param name="sender">The send of the event</param>
        /// <param name="e">The event data</param>
        private void _buttonDownloadShow_Click(object sender, RoutedEventArgs e)
        {
            _episodeData.DownloadAsMp3();
        }
    }
}
