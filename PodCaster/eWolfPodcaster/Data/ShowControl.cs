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

        internal XmlReader UpdateRSSFile()
        {
            XmlReader reader = null;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;

            reader = XmlReader.Create(RssFeed, settings);

            return reader;
        }

        internal void UpdateEpisode(List<EpisodeControl> episodes)
        {
        }
    }
}
