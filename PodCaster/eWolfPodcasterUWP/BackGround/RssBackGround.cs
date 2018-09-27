using eWolfPodcasterCore.Data;
using System.Threading;

namespace eWolfPodcasterUWP.BackGround
{
    internal class RssBackGround
    {
        private readonly Shows _shows;

        public RssBackGround(Shows shows)
        {
            _shows = shows;
        }

        internal void Runner()
        {
            while (true)
            {
                _shows.UpdateAllRSSFeeds();
                Thread.Sleep(1000);
            }
        }
    }
}
