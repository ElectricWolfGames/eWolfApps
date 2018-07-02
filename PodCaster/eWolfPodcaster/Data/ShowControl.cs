using eWolfPodcaster.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml;

namespace eWolfPodcaster.Data
{
    [Serializable]
    public class ShowControl : Show, ISaveable
    {
        public string GetFileName
        {
            get { return Title + ".Show"; }
        }

        internal void UpdateEpisode(List<EpisodeControl> newEpisodes)
        {
            foreach (EpisodeControl newEpisode in newEpisodes)
            {
                bool orignalEpisode = true;
                foreach (EpisodeControl oldEpisodes in Episodes)
                {
                    if (oldEpisodes.SameAs(newEpisode))
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

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;

            reader = XmlReader.Create(RssFeed, settings);

            return reader;
        }
    }
}
