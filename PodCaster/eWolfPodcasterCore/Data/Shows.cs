using eWolfPodcasterCore.Helpers;
using eWolfPodcasterCore.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;

namespace eWolfPodcasterCore.Data
{
    [Serializable]
    public class Shows
    {
        private ObservableCollection<ShowControl> _shows = new ObservableCollection<ShowControl>();

        public static Shows GetShowService
        {
            get
            {
                return ServiceLocator.Instance.GetService<Shows>();
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
                if (Contains(show))
                {
                    return false;
                }
                _shows.Add(show);
            }
            return true;
        }

        public void AddNewShow()
        {
            ShowControl sc = CreateFakeShow();
            sc.Title = "New Show";
            sc.RssFeed = "feed";
            Add(sc);
        }

        public bool Contains(ShowControl show)
        {
            if (_shows.Any((x) => x != null && x.RssFeed == show.RssFeed))
                return true;

            if (_shows.Any((x) => x != null && x.Title == show.Title))
                return true;

            return false;
        }

        public void CreateFakeList()
        {
            ShowControl sc = CreateFakeShow();
            _shows.Add(sc);

            sc = CreateFakeShowB();
            _shows.Add(sc);
        }

        public void Load(string outputFolder)
        {
            PersistenceHelper<ShowControl> ph = new PersistenceHelper<ShowControl>(outputFolder);
            _shows = ph.LoadData();

            if (_shows.Count == 0)
            {
                ShowControl sc = CreateFakeShow();
                _shows.Add(sc);
                ph.SaveData(_shows);
            }
        }

        public void RemoveShow(ShowControl itemToRemove)
        {
            lock (_shows)
            {
                _shows.Remove(itemToRemove);
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

        public void Save(string outputFolder)
        {
            lock (_shows)
            {
                PersistenceHelper<ShowControl> ph = new PersistenceHelper<ShowControl>(outputFolder);
                ph.SaveData(_shows);
            }
        }

        public void UpdateAllRSSFeeds()
        {
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
                            XmlReader RSSFeed = sc.UpdateRSSFile();
                            List<EpisodeControl> episodes = RSSHelper.ReadEpisodes(RSSFeed);
                            sc.UpdateEpisode(episodes);
                        }
                        catch
                        {
                            // can't read Feed
                        }
                    }
                }
            }
        }

        private ShowControl CreateFakeShow()
        {
            ShowControl sc = new ShowControl
            {
                Title = "CodingBlocks",
                RssFeed = "http://www.codingblocks.net/feed/podcast"
            };

            sc.ShowOption.AudoDownloadEpisodes = false;
            sc.ShowOption.Category = "Dev";
            sc.ShowOption.CheckforUpdates = true;
            sc.Episodes = new List<EpisodeControl>();

            return sc;
        }

        private ShowControl CreateFakeShowB()
        {
            ShowControl sc = new ShowControl
            {
                Title = "Other",
                RssFeed = "http://www.codingblocks.net"
            };

            sc.ShowOption.AudoDownloadEpisodes = false;
            sc.ShowOption.Category = "Music";
            sc.ShowOption.CheckforUpdates = true;
            sc.Episodes = new List<EpisodeControl>();

            return sc;
        }
    }
}
