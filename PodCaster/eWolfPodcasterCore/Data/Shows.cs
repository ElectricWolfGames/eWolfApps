using eWolfPodcasterCore.Helper;
using eWolfPodcasterCore.Helpers;
using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterCore.Logger;
using eWolfPodcasterCore.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;

namespace eWolfPodcasterCore.Data
{
    [Serializable]
    public class Shows : IShows
    {
        private string _outputFolder;
        private ObservableCollection<ShowControl> _shows = new ObservableCollection<ShowControl>();

        public static IShows GetShowService
        {
            get
            {
                return ServiceLocator.Instance.GetService<IShows>();
            }
        }

        public int Count
        {
            get
            {
                return _shows.Count;
            }
        }

        public ObservableCollection<ShowControl> ShowList
        {
            get
            {
                lock (_shows)
                {
                    return _shows;
                }
            }
        }

        public bool Add(ShowControl show)
        {
            lock (_shows)
            {
                show.Modifyed = true;
                if (Contains(show))
                {
                    return false;
                }
                _shows.Add(show);
            }
            return true;
        }

        public bool Contains(ShowControl show)
        {
            if (_shows.Any((x) => x != null && x.RssFeed == show.RssFeed))
                return true;

            if (_shows.Any((x) => x != null && x.Title == show.Title))
                return true;

            return false;
        }

        public ShowControl GetShowFromName(string showName)
        {
            return _shows.FirstOrDefault(x => x != null && x.Title == showName);
        }

        public List<string> Groups()
        {
            List<string> groups = new List<string>();
            foreach (var show in _shows)
            {
                if (show?.Catergery == null)
                    continue;

                groups.Add(show.Catergery.Name);
            }

            return groups.Distinct().ToList();
        }

        public void Load(string outputFolder)
        {
            _outputFolder = outputFolder;

            PersistenceHelper<ShowControl> ph = new PersistenceHelper<ShowControl>(outputFolder);
            _shows = ph.LoadData();

            FixPath();
        }

        public void RemoveEpisodeFromShow(string showName, string episodeName)
        {
            var show = GetShowFromName(showName);
            if (show == null)
            {
                return;
            }

            IEnumerable<EpisodeControl> episode = show.Episodes.Where(x => x.Title == episodeName);
            if (episode.Count() == 1)
            {
                episode.First().Hidden = true;
            }
        }

        public void RemoveShow(ShowControl itemToRemove)
        {
            lock (_shows)
            {
                _shows.Remove(itemToRemove);

                PersistenceHelper<ShowControl> ph = new PersistenceHelper<ShowControl>(_outputFolder);
                ph.DeleteFile(itemToRemove);
            }
        }

        public void ReplaceAllShows(Shows shows)
        {
            _shows.Clear();

            foreach (ShowControl sc in shows.ShowList)
            {
                _shows.Add(sc);
            }
        }

        public void Save()
        {
            lock (_shows)
            {
                PersistenceHelper<ShowControl> ph = new PersistenceHelper<ShowControl>(_outputFolder);
                ph.SaveData(_shows);
            }
        }

        public List<ShowControl> ShowInGroup(string groupName)
        {
            if (groupName == "Ungrouped")
            {
                var list = new List<ShowControl>();
                foreach (var show in _shows)
                {
                    if (show == null)
                        continue;
                    if (show.Catergery == null || show.Catergery.Name == "None")
                    {
                        list.Add(show);
                    }
                }
                return list;
            }
            return _shows.Where(x => x?.Catergery?.Name == groupName).ToList();
        }

        public void UpdateAllRSSFeeds()
        {
            DebugLog.LogInfo("Updating RSS");

            lock (_shows)
            {
                foreach (ShowControl sc in _shows)
                {
                    if (sc == null)
                        continue;

                    if (sc.ShowOption.CheckforUpdates)
                    {
                        try
                        {
                            UpdateShow(sc);
                        }
                        catch (Exception ex)
                        {
                            ServiceLocator.Instance.GetService<LoggerService>().AddError($"UpdateAllRSSFeeds: Error {ex.Message}");

                            Console.WriteLine("UpdateAllRSSFeeds: Error");
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }

        public bool UpdateNextRSSFeeds()
        {
            ShowControl nextShow = _shows.FirstOrDefault(x => x != null
                && !x.UpdatedRssTurn
                && (x.ShowOption.CheckforUpdates || x.ShowOption.ShowStorage == ShowStorageType.LocalStorage));

            if (nextShow != null)
            {
                lock (_shows)
                {
                    nextShow.UpdatedRssTurn = true;
                }

                UpdateShow(nextShow);
                return false;
            }

            var nextShowDownload = _shows.FirstOrDefault(x => x != null && !x.AutoDownloadTurn && x.ShowOption.AudoDownloadEpisodes);
            if (nextShowDownload != null)
            {
                lock (_shows)
                {
                    nextShowDownload.AutoDownloadTurn = true;
                    Console.WriteLine($"Download all shows for {nextShowDownload.Title}");
                    ServiceLocator.Instance.GetService<LoggerService>().AddError($"Auto download {nextShowDownload.Title}");

                    nextShowDownload.Download();
                    return false;
                }
            }

            Console.WriteLine("re set all shows to check for download");
            lock (_shows)
            {
                foreach (ShowControl show in _shows)
                {
                    if (show == null)
                        continue;

                    if (show.FailedCount < 2)
                    {
                        show.UpdatedRssTurn = false;
                        show.AutoDownloadTurn = false;
                    }
                }
                return true;
            }
        }

        public void UpdateShow(ShowControl sc)
        {
            Console.WriteLine("Updating RSS for " + sc.Title);
            if (sc.LocalFiles)
            {
                lock (_shows)
                {
                    sc.ScanLocalFilesOnly();
                }
            }
            else
            {
                XmlReader RSSFeed = sc.UpdateRSSFile();
                if (RSSFeed == null)
                {
                    Console.WriteLine("Failed to download rss feed");
                    sc.FailedCount++;
                    return;
                }

                try
                {
                    List<EpisodeControl> episodes = RSSHelper.ReadEpisodes(RSSFeed);
                    lock (_shows)
                    {
                        sc.UpdateEpisode(episodes);
                    }
                }
                catch { }
            }
        }

        private void FixPath()
        {
            foreach (var show in _shows)
            {
                if (show == null || show.RssFeed == null)
                    continue;

                if (show.RssFeed.StartsWith("G:"))
                {
                    show.RssFeed = show.RssFeed.Replace("G:", "H:");
                    show.ShowOption.CheckforUpdates = false;
                }

                foreach (var e in show.Episodes)
                {
                    if (e == null || e.PodcastURL == null)
                        continue;

                    if (e.PodcastURL.StartsWith("G:"))
                    {
                        e.PodcastURL = e.PodcastURL.Replace("G:", "H:");
                    }
                }
            }
        }
    }
}