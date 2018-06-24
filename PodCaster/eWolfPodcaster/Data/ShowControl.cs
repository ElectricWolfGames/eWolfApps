using eWolfPodcaster.Interfaces;
using System;

namespace eWolfPodcaster.Data
{
    [Serializable]
    public class ShowControl : Show, ISaveable
    {
        public string GetFileName
        {
            get { return Title + ".Show"; }
        }
    }
}
