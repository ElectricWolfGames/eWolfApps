using System.Collections.Generic;
using System.Linq;

namespace eWolfPodcaster.Data
{
    public class Shows
    {
        private ICollection<Show> _shows = new List<Show>();

        public int Count
        {
            get { return _shows.Count; }
        }

        public void Add(Show show)
        {
            if (_shows.Where((x) => x.RssFeed == show.RssFeed).Any())
                return;

            if (_shows.Where((x) => x.Title == show.Title).Any())
                return;

            _shows.Add(show);
        }
    }
}
