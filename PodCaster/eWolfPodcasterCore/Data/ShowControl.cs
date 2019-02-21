using eWolfPodcasterCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace eWolfPodcasterCore.Data
{
    [Serializable]
    public class ShowControl : Show, ISaveable
    {
        public bool LocalFiles { get; set; }

        public string TitleCount
        {
            get
            {
                return Title + $" {Count}";
            }
        }

        public string GetFileName
        {
            get { return Title + ".Show"; }
        }

        public override string ToString()
        {
            return TitleCount;
        }

        internal void UpdateEpisode(List<EpisodeControl> newEpisodes)
        {
            foreach (EpisodeControl newEpisode in newEpisodes)
            {
                bool orignalEpisode = true;
                foreach (EpisodeControl episode in Episodes)
                {
                    if (episode.SameAs(newEpisode))
                    {
                        orignalEpisode = false;
                    }
                }
                if (orignalEpisode)
                    Episodes.Add(newEpisode);
            }
        }

        internal XmlReader UpdateRSSFile()
        {
            XmlReader reader = null;

            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse
            };

            try
            {
                reader = XmlReader.Create(RssFeed, settings);
            }
            catch (Exception ex)
            {
                Console.WriteLine("UpdateRSSFile: Error");
                Console.WriteLine(ex.Message);
            }

            return reader;
        }

        internal void ScanLocalFilesOnly()
        {
            // check for local files only.
            int i = 0;
            i++;
        }
    }
}
