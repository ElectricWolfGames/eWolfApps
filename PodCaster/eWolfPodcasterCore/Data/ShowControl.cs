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
        private int _failedCount = 0;

        public bool AutoDownloadTurn { get; internal set; }

        public int EpisodesWatched
        {
            get
            {
                return Episodes.Count(x => x.Watched);
            }
        }

        public int FailedCount
        {
            get
            {
                return _failedCount;
            }
            set
            {
                _failedCount = value;
            }
        }

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

        public bool Modifyed { get; set; }

        public string TitleCount
        {
            get
            {
                return Title + $" {Count}";
            }
        }

        public bool UpdatedRssTurn { get; internal set; }

        public void ClearHidden()
        {
            Episodes.ForEach(x => x.Hidden = false);
        }

        public override string ToString()
        {
            return TitleCount;
        }

        public void UnwatchAll()
        {
            Episodes.ForEach(x => x.PlayedLength = 0);
            Episodes.ForEach(x => x.Hidden = false);
            Episodes.ForEach(x => x.PlayedLengthScaled = 0);
            Modifyed = true;
        }

        internal void Download()
        {
            foreach (var ep in Episodes)
            {
                if (!ep.IsOffLine)
                {
                    ep.ShowName = Title;
                    ep.DownloadAsMp3();
                }
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
                    //string folderExtra = filename.Replace(folderLocation, "");
                    //string[] folders = folderExtra.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                    //string groupName = CreateNewGroup

                    string fileNameUpper = filename.ToUpper();
                    string exp = Path.GetExtension(fileNameUpper);
                    if (exp != ".MP3" && exp != ".MP4")
                        continue;

                    string shortFileName = GetLastFolder(filename);

                    EpisodeControl ep = new EpisodeControl();
                    ep.Description = showName + " : " + shortFileName;
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
                Modifyed = true;
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
                reader = null;
            }

            return reader;
        }

        private string GetDownloadFolder()
        {
            return ServiceLocator.Instance.GetService<IProjectDetails>().GetDownloadFolder();
        }

        private string GetLastFolder(string filename)
        {
            string[] part = filename.Split('\\');
            return part[part.Length - 2];
        }
    }
}