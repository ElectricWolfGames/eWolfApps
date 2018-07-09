using eWolfPodcasterCore.Helpers;
using System;

namespace eWolfPodcasterCore.Data
{
    [Serializable]
    public class EpisodeControl : Episode
    {
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

        public bool SameAs(EpisodeControl newEpisode)
        {
            if (Title == newEpisode.Title)
                return true;

            if (PodcastURL == newEpisode.PodcastURL)
                return true;

            return false;
        }

        public void SetPublishDate(string publisedData)
        {
            PublishedDate = DataHelper.ParseDate(publisedData);
        }
    }
}
