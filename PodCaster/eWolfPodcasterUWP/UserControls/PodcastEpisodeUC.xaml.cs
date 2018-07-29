using eWolfPodcasterCore.Interfaces;
using System;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;

namespace eWolfPodcasterUWP.UserControls
{
    public sealed partial class PodcastEpisodeUC : UserControl, IPodCastInfo, INotifyPropertyChanged
    {
        public PodcastEpisodeUC(IPodCastInfo item)
        {
            EpisodeData = item;
            DataContext = this;

            this.InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Description
        {
            get { return EpisodeData.Description; }
        }

        public IPodCastInfo EpisodeData { get; set; }

        public long PlayedLength
        {
            get
            {
                return EpisodeData.PlayedLength;
            }
            set
            {
                EpisodeData.PlayedLength = value;
            }
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
                OnPropertyChanged("PlayedLengthScaled");
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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
