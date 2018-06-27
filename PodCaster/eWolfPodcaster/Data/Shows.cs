using System.Collections.Generic;
using System.Linq;

namespace eWolfPodcaster.Data
{
    public class Shows
    {
        private List<ShowControl> _shows = new List<ShowControl>();

        public int Count
        {
            get { return _shows.Count; }
        }

        public void Add(ShowControl show)
        {
            if (_shows.Where((x) => x.RssFeed == show.RssFeed).Any())
                return;

            if (_shows.Where((x) => x.Title == show.Title).Any())
                return;

            _shows.Add(show);
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

        private ShowControl CreateFakeShow()
        {
            ShowControl sc = new ShowControl();
            sc.Title = "CodingBlocks";
            sc.RssFeed = "http://www.codingblocks.net/feed/podcast";
            sc.ShowOption.AudoDownloadEpisodes = false;
            sc.ShowOption.Category = "Dev";
            sc.ShowOption.CheckforUpdates = true;

            return sc;
        }
    }
}
