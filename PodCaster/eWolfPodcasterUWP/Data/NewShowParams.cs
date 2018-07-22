using eWolfPodcasterCore.Data;
using System;

namespace eWolfPodcasterUWP.Data
{
    public class NewShowParams
    {
        public string Title { get; set; }

        public string RssFeed { get; set; }

        public Shows Shows { get; set; }

        public Action SaveCall { get; set; }
    }
}
