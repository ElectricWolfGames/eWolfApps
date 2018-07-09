using eWolfPodcasterCore.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;

namespace eWolfPodcasterCore.Data
{
    public class Shows
    {
        private ObservableCollection<ShowControl> _shows = new ObservableCollection<ShowControl>();

        public int Count
        {
            get { return _shows.Count; }
        }

        public ObservableCollection<ShowControl> ShowList
        {
            get { return _shows; }
        }

        public void Add(ShowControl show)
        {
            if (_shows.Any((x) => x.RssFeed == show.RssFeed))
                return;

            if (_shows.Any((x) => x.Title == show.Title))
                return;

            _shows.Add(show);
        }

        public void AddNewShow()
        {
            ShowControl sc = CreateFakeShow();
            sc.Title = "New Show";
            sc.RssFeed = "feed";
            Add(sc);
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
            _shows.Remove(itemToRemove);
        }

        public void Save(string outputFolder)
        {
            PersistenceHelper<ShowControl> ph = new PersistenceHelper<ShowControl>(outputFolder);
            ph.SaveData(_shows);
        }

        public void UpdateAllRSSFeeds()
        {
            foreach (ShowControl sc in _shows)
            {
                if (sc.ShowOption.CheckforUpdates)
                {
                    XmlReader RSSFeed = sc.UpdateRSSFile();
                    List<EpisodeControl> episodes = RSSHelper.ReadEpisodes(RSSFeed);
                    sc.UpdateEpisode(episodes);
                }
            }
        }

        private ShowControl CreateFakeShow()
        {
            ShowControl sc = new ShowControl();
            sc.Title = "CodingBlocks";
            sc.RssFeed = "http://www.codingblocks.net/feed/podcast";
            sc.ShowOption.AudoDownloadEpisodes = false;
            sc.ShowOption.Category = "Dev";
            sc.ShowOption.CheckforUpdates = true;
            sc.Episodes = new List<EpisodeControl>();

            return sc;
        }
    }
}
