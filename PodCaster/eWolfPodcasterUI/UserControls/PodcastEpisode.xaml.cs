using eWolfPodcasterCore.Interfaces;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace eWolfPodcasterUI.CustomDialog
{
    /// <summary>
    /// Interaction logic for PodcastEpisode.xaml
    /// </summary>
    public partial class PodcastEpisode : UserControl, IPodCastInfo
    {
        /// <summary>
        /// Podcast episode
        /// </summary>
        /// <param name="model">The main model data</param>
        public PodcastEpisode()
        {
            InitializeComponent();
            // _mainWindowModel = model;
        }

        /// <summary>
        /// The name / title of the podcast
        /// </summary>
        public string Title
        {
            get { return _episodeData.Title; }
        }

        /// <summary>
        /// The time the podcast was published
        /// </summary>
        public string PodCastTime
        {
            get { return _episodeData.PublishedDate.ToLongDateString(); }
        }

        /// <summary>
        /// The description of the pod cast
        /// </summary>
        public string Description
        {
            get { return _episodeData.Description; }
        }

        /// <summary>
        /// The downloadOption
        /// </summary>
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

        /// <summary>
        /// The pod cast url - stream or local
        /// </summary>
        public string PodcastURL
        {
            get { return _episodeData.PodcastURL; }
        }

        /// <summary>
        /// THe current played length
        /// </summary>
        public long PlayedLength
        {
            get { return _episodeData.PlayedLength; }
            set { _episodeData.PlayedLength = value; }
        }

        /// <summary>
        /// The current player length in the UI
        /// </summary>
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

        /// <summary>
        /// The Episode data
        /// </summary>
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

        public DateTime PublishedDate
        {
            get
            {
                return EpisodeData.PublishedDate;
            }
        }

        /// <summary>
        /// Check to see if the podcost in on the local drive.
        /// </summary>
        /// <returns>True is we can find the offline version</returns>
        public bool IsOffLine()
        {
            return EpisodeData.IsOffLine();
        }

        /// <summary>
        /// Gets the full file name for the offline version of the file
        /// </summary>
        /// <returns>The full offline filename</returns>
        public string GetOffLineFileName()
        {
            return EpisodeData.GetOffLineFileName();
        }

        /// <summary>
        /// On property changed event
        /// </summary>
        /// <param name="name">The name of the property that has changed</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// User has click on the remove button
        /// </summary>
        /// <param name="sender">The send of the event</param>
        /// <param name="e">The event data</param>
        private void _butRemove_Click(object sender, RoutedEventArgs e)
        {
            /*var button = sender as Button;
            if (button != null)
            {
                _mainWindowModel.Podcasts.Remove(this);
                _mainWindowModel.CurrentShow.RemoveEpisode(this);
            }
            else
            {
                return;
            }*/
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

        public void DownloadAsMp3()
        {
            throw new NotImplementedException();
        }

        #region Private Fields

        /// <summary>
        /// The property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The episode data to show
        /// </summary>
        private IPodCastInfo _episodeData;

        #endregion Private Fields
    }
}
