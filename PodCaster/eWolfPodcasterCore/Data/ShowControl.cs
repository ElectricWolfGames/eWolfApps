using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterCore.Logger;
using eWolfPodcasterCore.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace eWolfPodcasterCore.Data
{
    [Serializable]
    public class ShowControl : Show, ISaveable
    {
        [NonSerialized]
        private bool _updatedRss = false;

        public string GetFileName
        {
            get { return Title + ".Show"; }
        }

        public bool LocalFiles { get; set; }

        public string TitleCount
        {
            get
            {
                return Title + $" {Count}";
            }
        }

        public bool UpdatedRss1 { get => _updatedRss; set => _updatedRss = value; }
        public bool UpdatedRss { get; internal set; }

        public override string ToString()
        {
            return TitleCount;
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
                // safty catch
            }
            UpdateEpisode(episodes);
        }

        internal void UpdateEpisode(List<EpisodeControl> newEpisodes)
        {
            bool addedNew = false;
            foreach (EpisodeControl newEpisode in newEpisodes)
            {
                bool newEpisodeFlag = true;
                foreach (EpisodeControl episode in Episodes)
                {
                    if (episode.SameAs(newEpisode))
                    {
                        newEpisodeFlag = false;
                    }
                }

                if (newEpisodeFlag)
                {
                    Episodes.Add(newEpisode);
                    addedNew = true;
                }
            }

            if (addedNew)
            {
                DebugLog.LogInfo($"Added episodes to {Title}");
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
                //DebugLog.LogError($"UpdateRSSFile: Failed with {ex.Message}");
                Console.WriteLine(ex.Message);
            }

            return reader;
        }
    }
}
