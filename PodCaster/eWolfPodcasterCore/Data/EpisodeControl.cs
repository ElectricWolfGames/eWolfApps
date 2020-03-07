using eWolfPodcasterCore.Helpers;
using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterCore.Services;
using System;
using System.IO;
using System.Net;
using System.Threading;

namespace eWolfPodcasterCore.Data
{
    [Serializable]
    public class EpisodeControl : Episode, IPodCastInfo
    {
        public IEpisode EpisodeData
        {
            get; set;
        }

        public bool IsOffLine
        {
            get
            {
                string offLineFileName = GetOffLineFileName();
                return File.Exists(offLineFileName);
            }
        }

        public long PlayedLength
        {
            get
            {
                return PlayedDetails.PlayedLength;
            }
            set
            {
                PlayedDetails.PlayedLength = value;
            }
        }

        public double PlayedLengthScaled
        {
            get
            {
                return PlayedDetails.PlayedLengthScaled;
            }
            set
            {
                PlayedDetails.PlayedLengthScaled = value;
            }
        }

        public string ShowLength
        {
            get
            {
                return PlayedDetails.ShowLength.ToString("0:00");
            }
        }

        public string ShowName
        {
            get
            {
                return Show;
            }
            set
            {
                Show = value;
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

        public bool Watched
        {
            get
            {
                if (Hidden)
                    return true;

                if (PlayedDetails.Watched)
                    return true;

                double maxLength = 781;

                if (PlayedDetails.PlayedLengthScaled > maxLength - 50)
                    return true;

                return false;
            }
        }

        public void ClearDownload()
        {
            string downloadFile = GetOffLineFileName();
            if (File.Exists(downloadFile))
            {
                Console.WriteLine("Deleted downloaded file " + downloadFile);
                File.Delete(downloadFile);
            }
        }

        public void DownloadAsMp3()
        {
            var ds = DownloadService.GetDownloadService;
            string downloadFileTo = GetOffLineFileName();
            ds.Add(PodcastURL, downloadFileTo);
        }

        public string GetOffLineFileName()
        {
            string show = DataCleansing.FileSafeFileName(ShowName);
            string title = DataCleansing.FileSafeFileName(Title);

            return $"{GetDownloadFolder()}\\{show}\\{title}.mp3";
        }

        public bool SameAs(EpisodeControl newEpisode)
        {
            if (Title == newEpisode.Title)
                return true;

            if (PodcastURL == newEpisode.PodcastURL)
                return true;

            return false;
        }

        public void SetModifed()
        {
            var show = Shows.GetShowService.GetShowFromName(ShowName);
            show.Modifyed = true;
        }

        public void SetPublishDate(string publisedData)
        {
            PublishedDate = DateTimeHelper.ParseDate(publisedData);
        }

        public void SetTextNodeData(string elementName, string value)
        {
            switch (elementName)
            {
                case "title":
                    Title = value;
                    break;

                case "description":
                    Description = value;
                    break;

                case "itunes:summary":
                    Description = value;
                    break;

                case "pubDate":
                    SetPublishDate(value);
                    break;

                case "link":
                    if (string.IsNullOrWhiteSpace(PodcastURL))
                        PodcastURL = value;
                    break;

                case "itunes:duration":
                    string dur = value;
                    dur = dur.Replace(":", string.Empty);
                    double showLength;
                    if (double.TryParse(dur, out showLength))
                        PlayedDetails.ShowLength = showLength;

                    break;
            }
        }

        private string GetDownloadFolder()
        {
            return ServiceLocator.Instance.GetService<IProjectDetails>().GetDownloadFolder();
        }
    }
}