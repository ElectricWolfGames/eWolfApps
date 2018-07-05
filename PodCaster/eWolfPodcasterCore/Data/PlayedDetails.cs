using System;

namespace eWolfPodcasterCore.Data
{
    [Serializable]
    public class PlayedDetails
    {
        public long PlayedLength { get; set; }

        public double ShowLength { get; set; }

        public bool Watched { get; set; }
    }
}
