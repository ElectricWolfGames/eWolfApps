using eWolfPodcasterCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
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
            List<EpisodeControl> episodes = new List<EpisodeControl>();

            string folderLocation = RssFeed;
            try
            {
                string[] files = Directory.GetFiles(folderLocation, "*.*", SearchOption.AllDirectories);
                string showName = new DirectoryInfo(folderLocation).Name;
                foreach (string filename in files)
                {
                    string fileNameUpper = filename.ToUpper();
                    string exp = Path.GetExtension(fileNameUpper);
                    if (exp != ".MP3" && exp != ".MP4")
                        continue;

                    EpisodeControl ep = new EpisodeControl();
                    ep.Description = showName + " : " + filename;
                    ep.Hidden = false;
                    ep.PodcastURL = filename;
                    ep.PublishedDate = DateTime.Now;
                    ep.Title = Path.GetFileNameWithoutExtension(filename);

                    episodes.Add(ep);
                }
            }
            catch
            {
            }
            UpdateEpisode(episodes);
        }
    }
}
