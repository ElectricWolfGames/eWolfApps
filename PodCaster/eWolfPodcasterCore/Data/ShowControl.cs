using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterCore.Logger;
using eWolfPodcasterCore.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace eWolfPodcasterCore.Data
{
    [Serializable]
    public class ShowControl : Show, ISaveable
    {
        [NonSerialized]
        private readonly bool _updatedRss = false;

        public bool AutoDownloadTurn { get; internal set; }

        public string GetFileName
        {
            get { return Title + ".Show"; }
        }

        public bool LocalFiles
        {
            get
            {
                return ShowOption.ShowStorage == ShowStorageType.LocalStorage;
            }
        }

        public string TitleCount
        {
            get
            {
                return Title + $" {Count}";
            }
        }

        public bool UpdatedRssTurn { get; internal set; }

        public override string ToString()
        {
            return TitleCount;
        }

        internal void Download()
        {
            string downloadFolder = GetDownloadFolder();
            downloadFolder = Path.Combine(downloadFolder, Title);
            Directory.CreateDirectory(downloadFolder);

            for (int i = 0; i < 3; i++)
            {
                List<EpisodeControl> orderedEpisodes = Episodes.OrderBy(x => x.DownloadRetryCount).ToList();
                if (orderedEpisodes.Count == 0)
                    return;

                EpisodeControl ep = orderedEpisodes.First();
                ep.ShowName = Title;
                ep.DownloadAsMp3();
            }
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
                Shows.GetShowService.Save();
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

        private string GetDownloadFolder()
        {
            return ServiceLocator.Instance.GetService<IProjectDetails>().GetDownloadFolder();
        }
    }
}
