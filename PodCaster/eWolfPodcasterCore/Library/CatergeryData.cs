using System;

namespace eWolfPodcasterCore.Library
{
    [Serializable]
    public class CatergeryData
    {
        public CatergeryData(string catergery)
        {
            Name = catergery;
        }

        public string Name { get; set; }
    }
}